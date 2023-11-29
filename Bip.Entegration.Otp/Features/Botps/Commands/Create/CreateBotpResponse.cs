namespace Bip.Entegration.Otp.Features.Botps.Commands.Create;

public class CreateBotpResponse
{
    public string Data { get; set; }
}

public class CreateBotpInternalResponse
{
    public Guid TransactionId { get; set; }

    public string Key { get; set; }

    public string Target { get; set; }

    public DateTime LastValidTime { get; set; }
}
