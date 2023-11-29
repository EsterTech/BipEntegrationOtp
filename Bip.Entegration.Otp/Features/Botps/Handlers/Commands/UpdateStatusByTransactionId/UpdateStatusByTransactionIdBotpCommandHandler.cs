using AutoMapper;
using Bip.Entegration.Otp.DataAccess.Repositories.Contracts;
using Bip.Entegration.Otp.Features.Botps.Commands.UpdateStatusByTransactionId;
using Bip.Entegration.Otp.Features.Botps.Rules;
using Bip.Entegration.Otp.Models.Enums;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;

namespace Bip.Entegration.Otp.Features.Botps.Handlers.Commands.UpdateStatusByTransactionId;

public class UpdateStatusByTransactionIdBotpCommandHandler : IRequestHandler<ValidateOtpRequest, UpdateStatusByTransactionIdBotpResponse>
{
    private readonly BotpBusinesRules _botpBusinesRules;
    private readonly IBotpDal _botpDal;
    private readonly IMapper _mapper;
    public UpdateStatusByTransactionIdBotpCommandHandler(IBotpDal botpDal, BotpBusinesRules botpBusinesRules, IMapper mapper)
    {
        _botpDal = botpDal;
        _botpBusinesRules = botpBusinesRules;
        _mapper = mapper;
    }

    public async Task<UpdateStatusByTransactionIdBotpResponse> Handle(ValidateOtpRequest cipherRequest, CancellationToken cancellationToken)
    {
        var request = _botpBusinesRules.Decode<UpdateStatusByTransactionIdBotpCommand>(cipherRequest.Data);

        var otp = await _botpDal.GetAsync(w => w.Id == request.TransactionId && w.LastValidTime > DateTime.Now && w.BotpStatus == BotpStatus.Waiting);
        await _botpBusinesRules.ThrowExceptionIfDataNull(otp);

        if (!await _botpBusinesRules.ReturnFalseIfOtpInvalid(otp!, request))
        {
            if (otp!.TryCount == 2)
            {
                otp.BotpStatus = BotpStatus.Expired;
            }
            else
            {
                otp!.TryCount++;
            }


        }
        else
        {
            var oldStatus = otp!.BotpStatus;
            otp.BotpStatus = BotpStatus.Used;
            await _botpBusinesRules.ThrowOtpUsedLogEvent(otp, oldStatus);
        }

        await _botpDal.UpdateAsync(otp);

        if (otp.BotpStatus == BotpStatus.Expired)
        {
            throw new NotFoundException("Doğrulama Kodunuz İptal Edildi. Lütfen Tekrar Giriş Yapın");
        }

        return _mapper.Map<UpdateStatusByTransactionIdBotpResponse>(otp);
    }
}
