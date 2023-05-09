using Microsoft.EntityFrameworkCore;
using RiverTransportAutoschedule.Domain.Entities;
using RiverTransportAutoschedule.Domain.Repository.Abstractions;

namespace RiverTransportAutoschedule.Domain.Repository.EntityFramework;

public class EFScheduleRepository : IScheduleRepository
{

    private readonly ApplicationDbContext _context;

    public EFScheduleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async IAsyncEnumerable<Schedule> GetAllSchedulesAsync()
    {
        await foreach(var schedule in _context.Schedules.AsAsyncEnumerable())
            yield return schedule;
    }

    public async Task<Schedule> GetScheduleByIdAsync(Guid id)
    {
        return await _context.Schedules.FirstOrDefaultAsync(x => x.ScheduleId == id);
    }

    public async Task SaveScheduleAsync(Schedule schedule)
    {
        if (schedule.ScheduleId == default)
            _context.Entry(schedule).State = EntityState.Added;

        else
            _context.Entry(schedule).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteScheduleAsync(Guid id)
    {
        var schedule = await GetScheduleByIdAsync(id);

        _context.Schedules.Remove(schedule);

        await _context.SaveChangesAsync();
    }
}
