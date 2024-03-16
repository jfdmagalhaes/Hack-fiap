using Domain.Aggregates;
using MassTransit;

namespace Worker.Consumer;
public class MidiaReader : IConsumer<Midia>
{
    public async Task Consume(ConsumeContext<Midia> context)
    {
        var midia = new Midia
        {
            CreationData = context.Message.CreationData,
            //FormFile = context.Message.FormFile
        };

        //processar o video , armazenar

        throw new NotImplementedException();
    }
}
