using Bip.Entegration.Otp.Models.Enums;

namespace Bip.Entegration.Otp.Features.Botps.Queries.GetListDynamic;

public class GetListDynamicBotpResponse
{
    public Guid Id { get; set; }

    public string ClientId { get; set; }

    public string Target { get; set; }

    public BotpStatus BotpStatus { get; set; }

    public DateTime LastValidTime { get; set; }
}
