using Bip.Entegration.Otp.DataAccess.Context;
using Bip.Entegration.Otp.DataAccess.Repositories.Contracts;
using Bip.Entegration.Otp.Models;
using Core.Persistence.Repositories;

namespace Bip.Entegration.Otp.DataAccess.Repositories;

public class BotpLogDal : EfRepositoryBase<BotpLog, Guid, BipOtpDbContext>, IBotpLogDal
{
    public BotpLogDal(BipOtpDbContext context) : base(context)
    {
    }
}
