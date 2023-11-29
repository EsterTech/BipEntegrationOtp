using AutoMapper;
using Bip.Entegration.Otp.DataAccess.Repositories.Contracts;
using Bip.Entegration.Otp.Features.BotpLogs.Commands.Create;
using Bip.Entegration.Otp.Models;
using MediatR;

namespace Bip.Entegration.Otp.Features.BotpLogs.Handlers.Commands.Create;

public class CreateBotpLogCommandHandler : IRequestHandler<CreateBotpLogCommand, CreateBotpLogResponse>
{
    private readonly IBotpLogDal _botpLogDal;
    private readonly IMapper _mapper;

    public CreateBotpLogCommandHandler(IBotpLogDal botpLogDal, IMapper mapper)
    {
        _botpLogDal = botpLogDal;
        _mapper = mapper;
    }

    public async Task<CreateBotpLogResponse> Handle(CreateBotpLogCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<BotpLog>(request);

        await _botpLogDal.AddAsync(data);

        return _mapper.Map<CreateBotpLogResponse>(data);
    }
}
