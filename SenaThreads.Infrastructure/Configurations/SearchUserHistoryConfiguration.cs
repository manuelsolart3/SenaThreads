using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Configurations;
public class SearchUserHistoryConfiguration : IEntityTypeConfiguration<SearchUserHistory>
{
    public void Configure(EntityTypeBuilder<SearchUserHistory> builder)
    {
        builder.ToTable(nameof(SearchUserHistory));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SearchedAt).IsRequired(true);

        builder.HasOne(x => x.User)
        .WithMany()
        .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.SearchedUser)
       .WithMany()
       .HasForeignKey(x => x.SearchedUserId).OnDelete(DeleteBehavior.Restrict);

    }
}
