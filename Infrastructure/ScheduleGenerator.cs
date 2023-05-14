using RiverTransportAutoschedule.Domain;
using RiverTransportAutoschedule.Domain.Entities;

namespace RiverTransportAutoschedule.Infrastructure;

public class ScheduleGenerator
{
    private readonly DataManager _dataManager;

    public ScheduleGenerator(DataManager dataManager)
    {
        _dataManager = dataManager;
    }

    public async Task GenerateAndSaveScheduleAsync(WeatherConditions weather)
    {
        var riverTransports = _dataManager.Transports.GetAllRiverTransportsAsync();
        var riverPorts = _dataManager.Ports.GetAllRiverPortsAsync();

        var riverTransportsPorts = riverTransports
            .SelectMany(t => riverPorts, (transport, port) => (transport, port))
            .Distinct();

        await foreach (var (transport, port) in riverTransportsPorts)
        {
            var departureTime = await GenerateDepartureTimeAsync(DateTime.Today.AddDays(1), transport.RiverTransportId, port.RiverPortId);
            var arrivalTime = GenerateArrivalTime(departureTime);

            var schedule = new Schedule
            {
                ScheduleId = new Guid(),
                DepartureTime = departureTime,
                ArrivalTime = arrivalTime,
                RiverTransportId = transport.RiverTransportId,
                RiverPortId = port.RiverPortId,
            };

            var optimizedSchedule = await OptimizeScheduleAsync(schedule, weather);

            if (optimizedSchedule != null)
            {
                await _dataManager.Schedules.SaveScheduleAsync(optimizedSchedule);
            }
        }
    }

    private async Task<Schedule> OptimizeScheduleAsync(Schedule schedule, WeatherConditions weather)
    {
        if (!weather.IsWeatherConditionSatisfied(schedule))
        {
            return null;
        }

        var schedulesForTransport = _dataManager.Schedules.GetAllSchedulesAsync().Where(s => s.RiverTransportId == schedule.RiverTransportId);

        var schedulesForPort = _dataManager.Schedules.GetAllSchedulesAsync().Where(s => s.RiverPortId == schedule.RiverPortId);

        var intersectingSchedules = schedulesForTransport.Intersect(schedulesForPort);

        await foreach (var intersectingSchedule in intersectingSchedules)
        {
            if (schedule.DepartureTime >= intersectingSchedule.DepartureTime && schedule.DepartureTime < intersectingSchedule.ArrivalTime ||schedule.ArrivalTime > intersectingSchedule.DepartureTime && schedule.ArrivalTime <= intersectingSchedule.ArrivalTime)
            {
                return null;
            }
        }

        return schedule;
    }

    private async Task<DateTime> GenerateDepartureTimeAsync(DateTime date, Guid transportId, Guid portId)
    {
        var random = new Random();
        var hours = Enumerable.Range(8, 12 - 8 + 1).Where(h => h >= 8 && h < 20).ToList();

        // Повторное время отправления не раньше 6:00 и не позже 20:00
        var earliestDepartureTime = new DateTime(date.Year, date.Month, date.Day, 6, 0, 0);
        var latestDepartureTime = new DateTime(date.Year, date.Month, date.Day, 20, 0, 0);

        // Отправление через 6 часов
        var minDepartureTime = DateTime.Today.AddDays(1).AddHours(6);

        // Получаем все расписания для данного транспорта и порта на выбранную дату
        var schedules = await _dataManager.Schedules.GetAllSchedulesAsync().ToListAsync();
        var existingSchedules = schedules.Where(s => s.RiverTransportId == transportId && s.RiverPortId == portId && s.DepartureTime.Date == date.Date);

        while (true)
        {
            // Генерируем случайное время отправления
            var randomHour = hours[random.Next(hours.Count)];
            var departureTime = new DateTime(date.Year, date.Month, date.Day, randomHour, 0, 0).AddMinutes(random.Next(0, 2) * 30);

            // Проверяем, что время отправления соответствует условиям
            if (departureTime >= earliestDepartureTime && departureTime <= latestDepartureTime && departureTime >= minDepartureTime)
            {
                // Проверяем, что время отправления не пересекается с уже существующими расписаниями
                if (!existingSchedules.Any(s => departureTime >= s.DepartureTime && departureTime < s.ArrivalTime || departureTime.AddHours(3) > s.DepartureTime && departureTime.AddHours(3) <= s.ArrivalTime))
                {
                    return departureTime;
                }
            }
        }
    }

    private static DateTime GenerateArrivalTime(DateTime departureTime)
    {
        return departureTime.AddHours(3);
    }
}