using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Infrastructure.Repositories;

namespace SenaThreads.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        //DbContext y cc de conexion
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IUnitOfWork>(provider => provider.GetService<AppDbContext>());

        //reposiotrios
        services.AddScoped<ITweetRepository, TweetRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IEventRepository, EventRepository>();

        return services;
    }
}
