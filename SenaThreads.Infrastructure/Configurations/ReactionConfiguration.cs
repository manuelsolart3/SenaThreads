using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Configurations;

public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
{
    public void Configure(EntityTypeBuilder<Reaction> builder)
    {
        builder.ToTable(nameof(Reaction));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Type).HasConversion<int>();
        builder.HasOne(x => x.Tweet).WithMany(y => y.Reactions).HasForeignKey(x => x.TweetId);
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
    }
}
