using AutoMapper;
using Bip.Entegration.Otp.Features.BotpLogs.Commands.Create;
using Bip.Entegration.Otp.Features.BotpLogs.Queries.GetListDynamic;
using Bip.Entegration.Otp.Models;
using Core.Persistence.Models.Responses;
using Core.Persistence.Paging;

namespace Bip.Entegration.Otp.Features.BotpLogs.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBotpLogCommand, BotpLog>();
        CreateMap<BotpLog, CreateBotpLogResponse>();

        CreateMap<BotpLog, GetListDynamicBotpLogResponse>();
        CreateMap<Paginate<BotpLog>, ListModel<GetListDynamicBotpLogResponse>>();
    }
}

