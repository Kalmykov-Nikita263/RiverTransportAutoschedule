using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RiverTransportAutoschedule.Domain;
using RiverTransportAutoschedule.Domain.Entities;
using RiverTransportAutoschedule.Infrastructure;

namespace RiverTransportAutoschedule.Controllers;

public class ScheduleController : Controller
{
    private readonly DataManager _dataManager;
    private readonly ScheduleGenerator _scheduleGenerator;

    public ScheduleController(DataManager dataManager, ScheduleGenerator scheduleGenerator)
    {
        _dataManager = dataManager;
        _scheduleGenerator = scheduleGenerator;
    }

    public IActionResult Index()
    {
        return View(_dataManager.Schedules.GetAllSchedulesAsync());
    }

    public IActionResult GenerateSchedule()
    {
        return View(new WeatherConditions());
    }

    [HttpPost]
    public async Task<IActionResult> GenerateSchedule(WeatherConditions weather)
    {
        if (ModelState.IsValid)
        {
            await _scheduleGenerator.GenerateAndSaveScheduleAsync(weather);
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var schedule = await _dataManager.Schedules.GetScheduleByIdAsync(id);
        return View(schedule);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Schedule schedule)
    {
        if (ModelState.IsValid)
        {
            await _dataManager.Schedules.SaveScheduleAsync(schedule);
            return RedirectToAction("Index", "Schedule");
        }

        return View();
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _dataManager.Schedules.DeleteScheduleAsync(id);
        return RedirectToAction("Index", "Schedule");
    }

    [HttpPost]
    public async Task<IActionResult> ExportSchedule()
    {
        var schedules = _dataManager.Schedules.GetAllSchedulesAsync();

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Schedule");

        // Заполнение таблицы данными из списка расписаний
        worksheet.Cell(1, 1).Value = "Маршрут";
        worksheet.Cell(1, 2).Value = "Время отправления";
        worksheet.Cell(1, 3).Value = "Время прибытия";
        worksheet.Cell(1, 4).Value = "Название судна";

        int row = 2;

        await foreach (var schedule in schedules.OrderBy(s => s.DepartureTime))
        {
            worksheet.Cell(row, 1).Value = schedule.RiverTransport.Route;
            worksheet.Cell(row, 2).Value = schedule.DepartureTime.ToString("dd.MM.yyyy HH:mm");
            worksheet.Cell(row, 3).Value = schedule.ArrivalTime.ToString("dd.MM.yyyy HH:mm");
            worksheet.Cell(row, 4).Value = schedule.RiverTransport.Name;
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        // Возврат файла пользователю
        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "schedule.xlsx");
    }
}