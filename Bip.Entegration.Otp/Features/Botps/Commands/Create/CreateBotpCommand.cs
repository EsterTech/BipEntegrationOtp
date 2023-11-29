using MediatR;

namespace Bip.Entegration.Otp.Features.Botps.Commands.Create;

public class CreateBotpRequest : IRequest<CreateBotpResponse>
{
    public string Data { get; set; }
}

public class CreateBotpInternalCommand : IRequest<CreateBotpInternalResponse>
{
    public string Target { get; set; }

    public string ClientId { get; set; }
}
