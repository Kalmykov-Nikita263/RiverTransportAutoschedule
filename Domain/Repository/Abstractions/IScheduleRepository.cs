using RiverTransportAutoschedule.Domain.Entities;

namespace RiverTransportAutoschedule.Domain.Repository.Abstractions;

public interface IScheduleRepository
{
    IAsyncEnumerable<Schedule> GetAllSchedulesAsync();

    Task<Schedule> GetScheduleByIdAsync(Guid id);

    Task SaveScheduleAsync(Schedule schedule);

    Task DeleteScheduleAsync(Guid id);
}
