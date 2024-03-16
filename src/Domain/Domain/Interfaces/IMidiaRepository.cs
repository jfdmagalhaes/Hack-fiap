using Domain.Aggregates;

namespace Domain.Interfaces;
public interface IMidiaRepository : IDisposable
{
    Task AddAndCommitMidia(Midia midia);
    Task<IEnumerable<Midia>> GetMidiasAsync();
}