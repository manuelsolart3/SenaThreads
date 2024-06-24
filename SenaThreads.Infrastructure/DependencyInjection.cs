using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Infrastructure.ExternalServices;
using SenaThreads.Infrastructure.Repositories;

namespace SenaThreads.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = null;
        var profile = configuration.GetValue<string>("Profile") ?? 
                      throw new ArgumentNullException(nameof(configuration));
        
        Console.WriteLine(profile);
        
        if (profile.Equals("prod"))
        {
            Console.WriteLine(Env.GetString("ConnectionString"));
            connectionString = Env.GetString("ConnectionString") ??
                               throw new ArgumentNullException(nameof(configuration));
        }
        else if (profile.Equals("local"))
        {
            connectionString = configuration.GetSection("ConnectionStrings").GetValue<string>("Database") ??
                               throw new ArgumentNullException(nameof(configuration));   
        }
        else
        {
            throw new ArgumentNullException(nameof(configuration)); 
        }
        
        //DbContext y cc de conexion
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        
        // External Services
        services.AddScoped<IAwsS3Service, AwsS3Service>();

        //reposiotrios
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITweetRepository, TweetRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IUserBlockRepository, UserBlockRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IReactionRepository, ReactionRepository>();
        services.AddScoped<IRetweetRepository,RetweetRepository>();

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AppDbContext>());

        return services;
    }
}
