using Domain.Aggregates;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Infrastructure.EntityFramework.Context;
public interface IMidiaDbContext
{
    IDbConnection Connection { get; }
    DbSet<Midia> Midias { get; set; }
    Task<bool> CommitAsync();
    IDbContextTransaction GetCurrentTransaction();
}