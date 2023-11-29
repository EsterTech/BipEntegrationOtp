using Bip.Entegration.Otp.DataAccess.Repositories.Contracts;
using Bip.Entegration.Otp.Features.Botps.Commands.Create;
using Bip.Entegration.Otp.Features.Botps.Rules;
using MediatR;

namespace Bip.Entegration.Otp.Features.Botps.Handlers.Commands.Create;

public class CreateBotpCommandHandler : IRequestHandler<CreateBotpRequest, CreateBotpResponse>
{
    private readonly BotpBusinesRules _botpBusinesRules;
    private readonly IBotpDal _botpDal;

    public CreateBotpCommandHandler(IBotpDal botpDal, BotpBusinesRules botpBusinesRules)
    {
        _botpDal = botpDal;
        _botpBusinesRules = botpBusinesRules;
    }

    public async Task<CreateBotpResponse> Handle(CreateBotpRequest cipherRequest, CancellationToken cancellationToken)
    {
        var request = _botpBusinesRules.Decode<CreateBotpInternalCommand>(cipherRequest.Data);

        _botpBusinesRules.ThrowExceptionIfUserNotAuthorizedForClient(request.ClientId);

        var otpData = _botpBusinesRules.GenerateOtp();

        var botp = await _botpBusinesRules.CreateWaitingBotp(request.Target, otpData.SecretHash, request.ClientId);
        await _botpDal.AddAsync(botp);


        var toEncrpyt = new CreateBotpInternalResponse
        {
            Key = Convert.ToBase64String(otpData.SecretSalt),
            Target = request.Target,
            TransactionId = botp.Id,
            LastValidTime = botp.LastValidTime,
        };

        var returnData = new CreateBotpResponse
        {
            Data = _botpBusinesRules.Encrypt(toEncrpyt)
        };

        await _botpBusinesRules.SendOtpToTarget(otpData.Code, request.Target, request.ClientId);

        return returnData;

    }
}


public class CreateBotpInternalCommandHandler : IRequestHandler<CreateBotpInternalCommand, CreateBotpInternalResponse>
{
    private readonly BotpBusinesRules _botpBusinesRules;
    private readonly IBotpDal _botpDal;

    public CreateBotpInternalCommandHandler(IBotpDal botpDal, BotpBusinesRules botpBusinesRules)
    {
        _botpDal = botpDal;
        _botpBusinesRules = botpBusinesRules;
    }

    public async Task<CreateBotpInternalResponse> Handle(CreateBotpInternalCommand request, CancellationToken cancellationToken)
    {
        var otpData = _botpBusinesRules.GenerateOtp();

        var botp = await _botpBusinesRules.CreateWaitingBotp(request.Target, otpData.SecretHash, request.ClientId);
        await _botpDal.AddAsync(botp);

        var toEncrpyt = new CreateBotpInternalResponse
        {
            Key = Convert.ToBase64String(otpData.SecretSalt),
            Target = request.Target,
            TransactionId = botp.Id,
            LastValidTime = botp.LastValidTime,
        };
        await _botpBusinesRules.SendOtpToTarget(otpData.Code, request.Target, request.ClientId);

        return toEncrpyt;
    }
}

public class BotpGeneratedValues
{
    public string Code { get; set; }

    public byte[] SecretHash { get; set; }

    public byte[] SecretSalt { get; set; }
}
