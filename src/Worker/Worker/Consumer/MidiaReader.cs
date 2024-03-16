using Domain.Aggregates;
using Domain.Interfaces;
using MassTransit;

namespace Worker.Consumer;
public class MidiaReader : IConsumer<Midia>
{
    private readonly IMidiaRepository _repository;

    public MidiaReader(IMidiaRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Midia> context)
    {
        var midia = new Midia
        {
            CreationDate = context.Message.CreationDate,
            FilePath = context.Message.FilePath
        };

        await _repository.AddAndCommitMidia(midia);
    }
}