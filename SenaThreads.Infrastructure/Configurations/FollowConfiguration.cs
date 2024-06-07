using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Configurations;
public class FollowConfiguration : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
       builder.ToTable(nameof(Follow));
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.FollowerUser)
        .WithMany(u => u.Followers)
        .HasForeignKey(x => x.FollowerUserId);

        builder.HasOne(x => x.FollowedByUser)
               .WithMany(u => u.Followees)
               .HasForeignKey(x => x.FollowedByUserId);
    }
}
