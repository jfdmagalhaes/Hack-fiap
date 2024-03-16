using Domain.Aggregates;

namespace Domain.Interfaces;
public interface IMidiaProducer
{
    Task SendMessageAsync(Midia midia);
}