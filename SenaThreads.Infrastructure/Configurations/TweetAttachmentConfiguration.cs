using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Configurations;
public class TweetAttachmentConfiguration : IEntityTypeConfiguration<TweetAttachment>
{
    public void Configure(EntityTypeBuilder<TweetAttachment> builder)
    {
       builder.ToTable(nameof(TweetAttachment));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Key).HasMaxLength(255).IsRequired(true);
        builder.HasOne(x => x.Tweet).WithMany(y => y.Attachments).HasForeignKey(x => x.TweetId);
    }
}
