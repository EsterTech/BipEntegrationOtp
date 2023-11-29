using MediatR;

namespace Bip.Entegration.Otp.Features.Botps.Commands.UpdateStatusByTransactionId;

public class ValidateOtpRequest : IRequest<UpdateStatusByTransactionIdBotpResponse>
{
    public string Data { get; set; }
}

public class UpdateStatusByTransactionIdBotpCommand
{
    public Guid TransactionId { get; set; }

    public string Key { get; set; }

    public string Target { get; set; }

    public string Code { get; set; }
}
