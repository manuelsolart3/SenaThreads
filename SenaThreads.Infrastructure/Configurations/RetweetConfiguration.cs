using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaThreads.Domain.Tweets;


namespace SenaThreads.Infrastructure.Configurations;
public class RetweetConfiguration : IEntityTypeConfiguration<Retweet>
{
    public void Configure(EntityTypeBuilder<Retweet> builder)
    {
        builder.ToTable(nameof(Retweet));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Comment).HasMaxLength(300);
        builder.HasOne(x => x.Tweet).WithMany(y => y.Retweets).HasForeignKey(y => y.TweetId);
        builder.HasOne(x => x.RetweetedBy).WithMany().HasForeignKey(z => z.RetweetedById);
    }
}
