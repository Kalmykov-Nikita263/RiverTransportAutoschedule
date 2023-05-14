using RiverTransportAutoschedule.Domain.Entities;

namespace RiverTransportAutoschedule.Infrastructure;

public class WeatherConditions
{
    public int? AirTemperature { get; set; }

    public int? WindSpeed { get; set; }

    public WeatherType WeatherType { get; set; }

    public bool IsWeatherConditionSatisfied(Schedule schedule) =>
        (AirTemperature, WindSpeed, WeatherType, schedule.DepartureTime.Hour) switch
        {
            var (temp, _, _, _) when temp < -10 || temp > 35 => false,
            var (_, wind, _, _) when wind > 15 => false,
            (_, _, WeatherType.Rainy, _) => true,
            (_, _, WeatherType.Foggy, _) => false,
            var (_, _, _, hour) when hour >= 20 || hour < 6 => false,
            _ => true
        };
}