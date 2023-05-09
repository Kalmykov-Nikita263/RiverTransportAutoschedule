using Microsoft.EntityFrameworkCore;
using RiverTransportAutoschedule.Domain.Entities;
using RiverTransportAutoschedule.Domain.Repository.Abstractions;

namespace RiverTransportAutoschedule.Domain.Repository.EntityFramework;

public class EFRiverTransportRepository : IRiverTransportRepository
{
    private readonly ApplicationDbContext _context;

    public EFRiverTransportRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async IAsyncEnumerable<RiverTransport> GetAllRiverTransportsAsync()
    {
        await foreach(var riverTransport in _context.RiverTransports.AsAsyncEnumerable())
            yield return riverTransport;
    }

    public async Task<RiverTransport> GetRiverTransportByIdAsync(Guid id)
    {
        return await _context.RiverTransports.FirstOrDefaultAsync(x => x.RiverTransportId == id);
    }

    public async Task SaveRiverTransportAsync(RiverTransport riverTransport)
    {
        if (riverTransport.RiverTransportId == default)
            _context.Entry(riverTransport).State = EntityState.Added;

        else
            _context.Entry(riverTransport).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteRiverTransportById(Guid id)
    {
        var riverTransport = await GetRiverTransportByIdAsync(id);
        _context.RiverTransports.Remove(riverTransport);

        await _context.SaveChangesAsync();
    }
}