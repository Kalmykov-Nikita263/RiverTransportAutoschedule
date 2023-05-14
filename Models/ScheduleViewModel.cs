using RiverTransportAutoschedule.Domain.Entities;

namespace RiverTransportAutoschedule.Models;

public class ScheduleViewModel
{
    public string Route { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public RiverPort RiverPort { get; set; }

    public RiverTransport RiverTransport { get; set; }
}