using Microsoft.EntityFrameworkCore;
using SenaThreads.Domain.Events;
using SenaThreads.Domain.Notifications;
using SenaThreads.Domain.Tweets;
using SenaThreads.Domain.Users;
using SenaThreads.Infrastructure.Configurations;

namespace SenaThreads.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<UserBlock> UserBlocks { get; set; }
    public DbSet<Tweet> Tweets { get; set; }
    public DbSet<Retweet> Retweets { get; set; }
    public DbSet<TweetAttachment> TweetAttachments { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Event> Events { get; set; }

    //Constructos para configurar DbContextOptions
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    //Creamos el ModelCreate
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Con esto registramos la configuración de las entidades de manera automatica,
        // esto examina todas aquellas clases que hereden de IEntityTypeConfiguration

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(builder);
      


    }
}
