using Domain.Helpers;
using Domain.Interfaces;
using Infrastructure.EntityFramework.Context;
using Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Worker.Consumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>() ?? new AppSettings();

        services.AddSingleton(appSettings);
        services.AddDbContext<IMidiaDbContext, MidiaDbContext>(options => { options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); });
        services.AddTransient<IMidiaRepository, MidiaRepository>();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(appSettings.MassTransit.Servidor, "/", h =>
                {
                    h.Username(appSettings.MassTransit.Usuario);
                    h.Password(appSettings.MassTransit.Senha);
                });
                cfg.ReceiveEndpoint(appSettings.MassTransit.NomeFila, e =>
                {
                    e.Consumer<MidiaReader>(context);
                });
            });

            x.AddConsumer<MidiaReader>();
        });


        services.AddHostedService<Worker.Worker>();
    })
    .Build();

host.Run();