using Microsoft.EntityFrameworkCore;
using RiverTransportAutoschedule.Domain.Entities;
using RiverTransportAutoschedule.Domain.Repository.Abstractions;

namespace RiverTransportAutoschedule.Domain.Repository.EntityFramework;

public class EFRiverPortRepository : IRiverPortRepository
{

    private readonly ApplicationDbContext _context;

    public EFRiverPortRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async IAsyncEnumerable<RiverPort> GetAllRiverPortsAsync()
    {
        await foreach(var riverPort in _context.RiverPorts.AsAsyncEnumerable())
            yield return riverPort;
    }

    public async Task<RiverPort> GetRiverPortByIdAsync(Guid id)
    {
        return await _context.RiverPorts.FirstOrDefaultAsync(x => x.RiverPortId == id);
    }

    public async Task SaveRiverPortAsync(RiverPort riverPort)
    {
        if (riverPort.RiverPortId == default)
            _context.Entry(riverPort).State = EntityState.Added;

        else
            _context.Entry(riverPort).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteRiverPortAsync(Guid id)
    {
        var riverPort = await GetRiverPortByIdAsync(id);

        _context.RiverPorts.Remove(riverPort);

        await _context.SaveChangesAsync();
    }
}
