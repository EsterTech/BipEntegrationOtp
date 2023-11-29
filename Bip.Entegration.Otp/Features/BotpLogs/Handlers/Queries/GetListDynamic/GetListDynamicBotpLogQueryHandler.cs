using AutoMapper;
using Bip.Entegration.Otp.DataAccess.Repositories.Contracts;
using Bip.Entegration.Otp.Features.BotpLogs.Queries.GetListDynamic;
using Bip.Entegration.Otp.Features.BotpLogs.Rules;
using Core.Persistence.Models.Responses;
using MediatR;

namespace Bip.Entegration.Otp.Features.BotpLogs.Handlers.Queries.GetListDynamic;

public class GetListDynamicBotpLogQueryHandler : IRequestHandler<GetListDynamicBotpLogQuery, ListModel<GetListDynamicBotpLogResponse>>
{
    private readonly BotpLogBusinessRules _botpLogBusinessRules;
    private readonly IBotpLogDal _botpLogDal;
    private readonly IMapper _mapper;

    public GetListDynamicBotpLogQueryHandler(IMapper mapper, IBotpLogDal botpLogDal, BotpLogBusinessRules botpLogBusinessRules)
    {
        _mapper = mapper;
        _botpLogDal = botpLogDal;
        _botpLogBusinessRules = botpLogBusinessRules;
    }

    public async Task<ListModel<GetListDynamicBotpLogResponse>> Handle(GetListDynamicBotpLogQuery request, CancellationToken cancellationToken)
    {
        _botpLogBusinessRules.AddClientFilterIfUserNotSuperUser(request.DynamicQuery, "Botp.ClientId");
        var data = await _botpLogDal.GetListByDynamicAsync(request.DynamicQuery, w => w.BotpId == request.OtpId, size: request.PageRequest.PageSize, index: request.PageRequest.PageIndex, cancellationToken: cancellationToken);

        var returnData = _mapper.Map<ListModel<GetListDynamicBotpLogResponse>>(data);
        _botpLogBusinessRules.FillDynamicFilter(returnData, request.DynamicQuery, request.PageRequest);

        return returnData;
    }
}
