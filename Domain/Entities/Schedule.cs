namespace RiverTransportAutoschedule.Domain.Entities;

public class Schedule
{
    public Guid ScheduleId { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    //Навигационные свойства Entity Framework
    public Guid RiverTransportId { get; set; }

    public virtual RiverTransport RiverTransport { get; set; }

    public Guid RiverPortId { get; set; }

    public virtual RiverPort RiverPort { get; set; }
}
