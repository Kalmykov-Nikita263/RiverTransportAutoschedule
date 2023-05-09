namespace RiverTransportAutoschedule.Domain.Entities;

public class RiverPort
{
    public Guid RiverPortId { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public virtual List<Schedule> Schedules { get; set; }
}
