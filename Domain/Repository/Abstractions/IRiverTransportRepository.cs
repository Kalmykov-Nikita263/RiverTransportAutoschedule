using RiverTransportAutoschedule.Domain.Entities;

namespace RiverTransportAutoschedule.Domain.Repository.Abstractions
{
    public interface IRiverTransportRepository
    {
        IAsyncEnumerable<RiverTransport> GetAllRiverTransportsAsync();

        Task<RiverTransport> GetRiverTransportByIdAsync(Guid id);

        Task SaveRiverTransportAsync(RiverTransport riverTransport);

        Task DeleteRiverTransportById(Guid id);
    }
}
