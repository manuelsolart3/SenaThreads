using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaThreads.Domain.Events;

namespace SenaThreads.Infrastructure.Configurations;
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable(nameof(Event));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(50);
        builder.Property(x => x.Description).HasMaxLength(150);
        builder.Property(x => x.Image);
        builder.Property(x => x.EventDate);

        //Relacion con user
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}
