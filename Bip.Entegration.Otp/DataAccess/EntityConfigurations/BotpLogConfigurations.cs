using Bip.Entegration.Otp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bip.Entegration.Otp.DataAccess.EntityConfigurations;

public class BotpLogConfigurations : IEntityTypeConfiguration<BotpLog>
{
    public void Configure(EntityTypeBuilder<BotpLog> builder)
    {
        builder.ToTable("BotpLogs").HasKey(t => t.Id);

        builder.Property(w => w.Id).HasColumnName("Id").IsRequired();
        builder.Property(w => w.CreatedTime).HasColumnName("CreatedTime").IsRequired();
        builder.Property(w => w.DeletedTime).HasColumnName("DeletedTime");
        builder.Property(w => w.UpdatedTime).HasColumnName("UpdatedTime");

        builder.Property(w => w.BotpId).HasColumnName("BotpId").IsRequired();
        builder.Property(w => w.Description).HasColumnName("Description").IsRequired();
        builder.Property(w => w.From).HasColumnName("From").IsRequired();
        builder.Property(w => w.To).HasColumnName("To").IsRequired();

        builder.HasOne(w => w.Botp);

        builder.HasQueryFilter(w => !w.DeletedTime.HasValue);
    }
}
