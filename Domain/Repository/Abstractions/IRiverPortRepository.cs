using RiverTransportAutoschedule.Domain.Entities;

namespace RiverTransportAutoschedule.Domain.Repository.Abstractions;

public interface IRiverPortRepository
{
    IAsyncEnumerable<RiverPort> GetAllRiverPortsAsync();

    Task<RiverPort> GetRiverPortByIdAsync(Guid id);

    Task SaveRiverPortAsync(RiverPort riverPort);

    Task DeleteRiverPortAsync(Guid id);
}
