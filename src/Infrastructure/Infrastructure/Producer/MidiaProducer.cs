using Domain.Aggregates;
using Domain.Helpers;
using Domain.Interfaces;
using MassTransit;

namespace Infrastructure.Producer;
public class MidiaProducer : IMidiaProducer
{
    private readonly IBus _bus;
    private readonly AppSettings _appSettings;

    public MidiaProducer(IBus bus, AppSettings appSettings)
    {
        _bus = bus;
        _appSettings = appSettings;
    }

    public async Task SendMessageAsync(Midia midia)
    {
        Uri uri = new($"queue:{_appSettings.MassTransit.NomeFila}");
        var endPoint = await _bus.GetSendEndpoint(uri);

        await endPoint.Send(midia, CancellationToken.None);
    }
}