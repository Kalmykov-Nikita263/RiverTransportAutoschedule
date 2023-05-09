using RiverTransportAutoschedule.Domain.Repository.Abstractions;

namespace RiverTransportAutoschedule.Domain;

public class DataManager
{
    public IRiverTransportRepository RiverTransport { get; set; }

    public IScheduleRepository ScheduleRepository { get; set; }

    public IRiverPortRepository RiverPortRepository { get; set; }

    public DataManager(IRiverTransportRepository riverTransport, IScheduleRepository scheduleRepository, IRiverPortRepository riverPortRepository)
    {
        RiverTransport = riverTransport;
        ScheduleRepository = scheduleRepository;
        RiverPortRepository = riverPortRepository;
    }
}