﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Configurations;
public class UserBlockConfiguration : IEntityTypeConfiguration<UserBlock>
{
    public void Configure(EntityTypeBuilder<UserBlock> builder)
    {
        builder.ToTable(nameof(UserBlock));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.BlockSatus).HasConversion<int>();

        builder.HasOne(x => x.BlockedUser)
       .WithMany(u => u.BlockedUsers)
       .HasForeignKey(x => x.BlockedUserId);

        builder.HasOne(x => x.BlockByUser)
               .WithMany(u => u.BlockeByUsers)
               .HasForeignKey(x => x.BlockByUserId);

    }
}
