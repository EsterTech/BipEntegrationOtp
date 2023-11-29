using Bip.Entegration.Otp.Models.Enums;

namespace Bip.Entegration.Otp.Features.Botps.Commands.UpdateStatusByTransactionId;

public class UpdateStatusByTransactionIdBotpResponse
{
    public DateTime LastValidTime { get; set; }

    public int TryCount { get; set; }

    public BotpStatus BotpStatus { get; set; }
}
