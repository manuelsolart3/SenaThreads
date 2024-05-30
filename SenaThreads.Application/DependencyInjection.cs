using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SenaThreads.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        // Register Command Validators
        // Para usar la inyección de dependencias para los validadores de los Command,
        // es necesario haber instalado la librería 'FluentValidation.DependencyInjectionExtensions'
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;

    }
}
