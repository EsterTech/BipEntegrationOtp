using Bip.Entegration.Otp.Models.Enums;

namespace Bip.Entegration.Otp.Features.BotpLogs.Queries.GetListDynamic;

public class GetListDynamicBotpLogResponse
{
    public Guid BotpId { get; set; }

    public string Description { get; set; }

    public BotpStatus From { get; set; }

    public BotpStatus To { get; set; }

    public DateTime CreatedTime { get; set; }
}
