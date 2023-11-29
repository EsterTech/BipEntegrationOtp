using Bip.Entegration.Otp.DataAccess.Context;
using Bip.Entegration.Otp.DataAccess.Repositories.Contracts;
using Bip.Entegration.Otp.Models;
using Core.Persistence.Repositories;

namespace Bip.Entegration.Otp.DataAccess.Repositories;

public class BotpDal : EfRepositoryBase<Botp, Guid, BipOtpDbContext>, IBotpDal
{
    public BotpDal(BipOtpDbContext context) : base(context)
    {
    }
}
