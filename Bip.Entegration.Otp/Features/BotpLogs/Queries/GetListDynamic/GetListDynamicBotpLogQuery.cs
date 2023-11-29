using Bip.Entegration.Application.Base;
using Core.Persistence.Models.Responses;
using MediatR;

namespace Bip.Entegration.Otp.Features.BotpLogs.Queries.GetListDynamic;

public class GetListDynamicBotpLogQuery : BaseDynamicQuery, IRequest<ListModel<GetListDynamicBotpLogResponse>>
{
    public Guid OtpId { get; set; }

}
