using Bip.Entegration.Otp.Models.Enums;
using Core.Persistence.Models;

namespace Bip.Entegration.Otp.Models;

public class BotpLog : Entity<Guid>
{
    public Guid BotpId { get; set; }

    public virtual Botp Botp { get; set; }

    public string Description { get; set; }

    public BotpStatus From { get; set; }

    public BotpStatus To { get; set; }
}
