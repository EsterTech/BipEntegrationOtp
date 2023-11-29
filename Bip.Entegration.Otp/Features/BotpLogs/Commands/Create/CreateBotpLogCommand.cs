using Bip.Entegration.Otp.Models.Enums;
using MediatR;

namespace Bip.Entegration.Otp.Features.BotpLogs.Commands.Create;

public class CreateBotpLogCommand : IRequest<CreateBotpLogResponse>
{
    public Guid BotpId { get; set; }

    public string Description { get; set; }

    public BotpStatus From { get; set; }

    public BotpStatus To { get; set; }
}
