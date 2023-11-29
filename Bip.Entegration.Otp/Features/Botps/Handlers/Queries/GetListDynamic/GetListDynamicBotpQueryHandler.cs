using AutoMapper;
using Bip.Entegration.Otp.DataAccess.Repositories.Contracts;
using Bip.Entegration.Otp.Features.Botps.Queries.GetListDynamic;
using Bip.Entegration.Otp.Features.Botps.Rules;
using Core.Persistence.Models.Responses;
using MediatR;

namespace Bip.Entegration.Otp.Features.Botps.Handlers.Queries.GetListDynamic;

public class GetListDynamicBotpQueryHandler : IRequestHandler<GetListDynamicBotpQuery, ListModel<GetListDynamicBotpResponse>>
{
    private readonly BotpBusinesRules _botpBusinesRules;
    private readonly IBotpDal _botpDal;
    private readonly IMapper _mapper;

    public GetListDynamicBotpQueryHandler(IMapper mapper, IBotpDal botpDal, BotpBusinesRules botpBusinesRules)
    {
        _mapper = mapper;
        _botpDal = botpDal;
        _botpBusinesRules = botpBusinesRules;
    }

    public async Task<ListModel<GetListDynamicBotpResponse>> Handle(GetListDynamicBotpQuery request, CancellationToken cancellationToken)
    {
        _botpBusinesRules.AddClientFilterIfUserNotSuperUser(request.DynamicQuery);

        var data = await _botpDal.GetListByDynamicAsync(request.DynamicQuery, size: request.PageRequest.PageSize, index: request.PageRequest.PageIndex, cancellationToken: cancellationToken);
        var returnData = _mapper.Map<ListModel<GetListDynamicBotpResponse>>(data);

        _botpBusinesRules.FillDynamicFilter(returnData, request.DynamicQuery, request.PageRequest);

        return returnData;
    }
}
