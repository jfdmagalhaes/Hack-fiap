using Domain.Aggregates;

namespace Domain.Services;
public interface IMidiaService
{
    Task<IEnumerable<Midia>> GetAllMidias();
    Task SplitMidia(string midiaPath);
}