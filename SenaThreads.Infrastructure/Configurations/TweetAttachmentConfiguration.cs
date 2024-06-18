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
        builder.Property(x => x.Key).IsRequired(true);
    }
}
