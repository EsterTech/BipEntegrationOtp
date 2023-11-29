using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Bip.Entegration.Otp.DataAccess.Repositories.Contracts;
using Bip.Entegration.Otp.DataAccess.Context;
using Bip.Entegration.Otp.DataAccess.Repositories;

namespace Bip.Entegration.Otp;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<BipOtpDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IBotpDal, BotpDal>();
        services.AddScoped<IBotpLogDal, BotpLogDal>();
        return services;
    }
}
