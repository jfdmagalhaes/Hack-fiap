using Domain.Aggregates;
using Domain.Interfaces;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class MidiaRepository : IMidiaRepository
{
    private readonly IMidiaDbContext _context;

    public MidiaRepository(IMidiaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAndCommitMidia(Midia midia)
    {
        await _context.Midias.AddAsync(midia);
        await _context.CommitAsync();
    }

    public async Task<IEnumerable<Midia>> GetMidiasAsync()
    {
        return await _context.Midias.ToListAsync();
    }

    private bool disposedValue = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}