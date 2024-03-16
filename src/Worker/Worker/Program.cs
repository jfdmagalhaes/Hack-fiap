using Domain.Helpers;
using MassTransit;
using Worker.Consumer;

var builder = Host.CreateApplicationBuilder(args);

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>() ?? new AppSettings();

builder.Services.AddMassTransit(x =>
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

builder.Services.AddHostedService<Worker.Worker>();

var host = builder.Build();
host.Run();
