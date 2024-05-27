using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Configurations;
public class FollowConfiguration : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
       builder.ToTable(nameof(Follow));
        builder.HasOne(x => x.FollowerUser).WithMany().HasForeignKey(x => x.FollowerUserId);
        builder.HasOne(x => x.FollowedByUser).WithMany().HasForeignKey(x => x.FollowedByUserId);
    }
}
