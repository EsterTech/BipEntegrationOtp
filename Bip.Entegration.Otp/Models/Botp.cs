using Bip.Entegration.Otp.Models.Enums;
using Core.Persistence.Models;

namespace Bip.Entegration.Otp.Models;

public class Botp : Entity<Guid>
{
    public string ClientId { get; set; }

    public int TryCount { get; set; }

    public string Target { get; set; }

    public byte[] KeyHash { get; set; }

    public BotpStatus BotpStatus { get; set; }

    public DateTime LastValidTime { get; set; }

    public virtual ICollection<BotpLog> BotpLogs { get; set; }
}
