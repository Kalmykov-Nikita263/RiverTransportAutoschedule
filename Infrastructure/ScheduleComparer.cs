using RiverTransportAutoschedule.Domain.Entities;

namespace RiverTransportAutoschedule.Infrastructure;

public class ScheduleComparer : IEqualityComparer<Schedule>
{
    public bool Equals(Schedule x, Schedule y)
    {
        return x.DepartureTime == y.DepartureTime && x.ArrivalTime == y.ArrivalTime;
    }

    public int GetHashCode(Schedule obj)
    {
        return obj.DepartureTime.GetHashCode() ^ obj.ArrivalTime.GetHashCode();
    }
}