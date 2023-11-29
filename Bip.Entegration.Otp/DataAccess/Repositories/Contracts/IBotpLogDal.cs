using Bip.Entegration.Otp.Models;
using Core.Persistence.Repositories;

namespace Bip.Entegration.Otp.DataAccess.Repositories.Contracts;

public interface IBotpLogDal : IAsyncRepository<BotpLog, Guid>
{
}
