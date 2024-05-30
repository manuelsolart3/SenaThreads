using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).HasMaxLength(25).IsRequired(true);
        builder.Property(x => x.LastName).HasMaxLength(25).IsRequired(true);
        builder.Property(x => x.PhoneNumber).HasMaxLength(20);
        builder.Property(x => x.Biography).HasMaxLength(50);
        builder.Property(x => x.City).HasMaxLength(25);
        builder.Property(u => u.DateOfBirth);
    }
}
