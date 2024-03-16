using Domain.Helpers;
using Domain.Interfaces;
using Domain.Services;
using Infrastructure.EntityFramework.Context;
using Infrastructure.Producer;
using Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationExternalDependencies(this IServiceCollection services, AppSettings appSettings, IConfiguration configuration)
    {
        services.AddDbContext<IMidiaDbContext, MidiaDbContext>(options => { options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); });
        services.AddTransient<IMidiaRepository, MidiaRepository>();

        services.AddScoped<IMidiaProducer, MidiaProducer>();
        ConfigureMassTransit(appSettings, services);

        return services;
    }


    private static void ConfigureMassTransit(AppSettings appSettings, IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(appSettings.MassTransit.Servidor, "/", h =>
                {
                    h.Username(appSettings.MassTransit.Usuario);
                    h.Password(appSettings.MassTransit.Senha);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}