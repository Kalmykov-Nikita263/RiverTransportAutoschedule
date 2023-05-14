using RiverTransportAutoschedule.Domain.Repository.Abstractions;

namespace RiverTransportAutoschedule.Domain;

public class DataManager
{
    public IRiverTransportRepository Transports { get; set; }

    public IScheduleRepository Schedules { get; set; }

    public IRiverPortRepository Ports{ get; set; }

    public DataManager(IRiverTransportRepository riverTransport, IScheduleRepository scheduleRepository, IRiverPortRepository riverPortRepository)
    {
        Transports = riverTransport;
        Schedules = scheduleRepository;
        Ports = riverPortRepository;
    }
}