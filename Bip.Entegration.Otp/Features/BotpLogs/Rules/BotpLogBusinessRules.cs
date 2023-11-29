using Bip.Entegration.Otp.BaseRules;
using Core.ApiHelpers.JwtHelper.Models;

namespace Bip.Entegration.Otp.Features.BotpLogs.Rules;

public class BotpLogBusinessRules : BaseOtpBusinessRules
{
    public BotpLogBusinessRules(TokenParameters tokenParameters) : base(tokenParameters)
    {
    }
}
