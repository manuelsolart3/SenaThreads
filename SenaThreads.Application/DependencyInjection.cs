using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SenaThreads.Application.Abstractions.Behaviors;
using SenaThreads.Application.Authentication;

namespace SenaThreads.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        services.AddScoped<JwtService>();

        return services;

    }
}
