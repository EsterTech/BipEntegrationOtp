using Bip.Entegration.Application.Base;
using Core.ApiHelpers.JwtHelper.Models;

namespace Bip.Entegration.Otp.BaseRules;

public class BaseOtpBusinessRules : BaseBusinessRules
{
    public BaseOtpBusinessRules(TokenParameters tokenParameters) : base(tokenParameters)
    {
    }
}
