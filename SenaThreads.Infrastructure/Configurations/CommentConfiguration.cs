using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Configurations;
public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(nameof(Comment));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Text).HasMaxLength(180).IsRequired();
        builder.HasOne(x => x.Tweet).WithMany().HasForeignKey(x => x.TweetId);
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
    }
}
