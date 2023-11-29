using Bip.Entegration.Otp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bip.Entegration.Otp.DataAccess.EntityConfigurations;

public class BotpConfigurations : IEntityTypeConfiguration<Botp>
{
    public void Configure(EntityTypeBuilder<Botp> builder)
    {
        builder.ToTable("Botps").HasKey(t => t.Id);

        builder.Property(w => w.Id).HasColumnName("Id").IsRequired();
        builder.Property(w => w.CreatedTime).HasColumnName("CreatedTime").IsRequired();
        builder.Property(w => w.DeletedTime).HasColumnName("DeletedTime");
        builder.Property(w => w.UpdatedTime).HasColumnName("UpdatedTime");

        builder.Property(w => w.ClientId).HasColumnName("ClientId").IsRequired();
        builder.Property(w => w.TryCount).HasColumnName("TryCount").IsRequired();
        builder.Property(w => w.Target).HasColumnName("Target").IsRequired();
        builder.Property(w => w.KeyHash).HasColumnName("KeyHash").IsRequired();
        builder.Property(w => w.BotpStatus).HasColumnName("BotpStatus").IsRequired();
        builder.Property(w => w.LastValidTime).HasColumnName("LastValidTime").IsRequired();

        builder.HasMany(w => w.BotpLogs);

        builder.HasQueryFilter(w => !w.DeletedTime.HasValue);
    }
}
