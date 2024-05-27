using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Configurations;

public class TweetConfiguration : IEntityTypeConfiguration<Tweet>
{
    public void Configure(EntityTypeBuilder<Tweet> builder)
    {
        builder.ToTable(nameof(Tweet)); // Defino el nombre de la tabla en la BD
        builder.HasKey(x => x.Id);      // Defino cual es la llave primaria de la tabla
        builder.Property(x => x.Text).HasMaxLength(300);    // Definimos una propiedad 'Text' y le asignamos una restricción de longitud
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);    // Definimos la configuración de la llave foranea
       
    }
}

