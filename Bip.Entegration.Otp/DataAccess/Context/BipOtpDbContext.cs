using Bip.Entegration.Otp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Bip.Entegration.Otp.DataAccess.Context;

public class BipOtpDbContext : DbContext
{
    public BipOtpDbContext(DbContextOptions<BipOtpDbContext> opts) : base(opts)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Botp> Botps { get; set; }

    public virtual DbSet<BotpLog> BotpLogs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema("otp");
    }

}
