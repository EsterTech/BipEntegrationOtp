using Bip.Entegration.Application.Base;
using Core.Persistence.Models.Responses;
using MediatR;

namespace Bip.Entegration.Otp.Features.Botps.Queries.GetListDynamic;

public class GetListDynamicBotpQuery : BaseDynamicQuery, IRequest<ListModel<GetListDynamicBotpResponse>>
{
}
