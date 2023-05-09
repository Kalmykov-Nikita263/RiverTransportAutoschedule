namespace RiverTransportAutoschedule.Domain.Entities;

public class RiverTransport
{
    public Guid RiverTransportId { get; set; }

    public string Name { get; set; }

    public RiverTransportType TransportType { get; set; }

    public string Route { get; set; }

    public int Capacity { get; set; }

    public virtual List<Schedule> Schedules { get; set; }
}