using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Infrastructure.Repositories;

namespace SenaThreads.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database") ??
                                       throw new ArgumentNullException(nameof(configuration));

        //DbContext y cc de conexion
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        //reposiotrios
        services.AddScoped<ITweetRepository, TweetRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<IUserBlockRepository, UserBlockRepository>();

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AppDbContext>());

        return services;
    }
}
