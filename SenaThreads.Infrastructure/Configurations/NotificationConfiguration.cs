using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Infrastructure.Configurations;
public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable(nameof(Notification));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Path).IsRequired(true);
        builder.Property(x => x.Type).HasConversion<int>().IsRequired(true);
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);

    }
}
