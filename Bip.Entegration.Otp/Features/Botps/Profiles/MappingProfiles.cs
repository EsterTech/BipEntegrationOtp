using AutoMapper;
using Bip.Entegration.Otp.Features.Botps.Commands.UpdateStatusByTransactionId;
using Bip.Entegration.Otp.Features.Botps.Queries.GetListDynamic;
using Bip.Entegration.Otp.Models;
using Core.Persistence.Models.Responses;
using Core.Persistence.Paging;

namespace Bip.Entegration.Otp.Features.Botps.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Botp, GetListDynamicBotpResponse>();
        CreateMap<Paginate<Botp>, ListModel<GetListDynamicBotpResponse>>();

        CreateMap<Botp, UpdateStatusByTransactionIdBotpResponse>();

    }
}
