using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiverTransportAutoschedule.Domain;
using RiverTransportAutoschedule.Models;
using System.Diagnostics;

namespace RiverTransportAutoschedule.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly DataManager _dataManager;

    public HomeController(DataManager dataManager)
    {
        _dataManager = dataManager;
    }

    public IActionResult Index()
    {
        return View(_dataManager.Schedules.GetAllSchedulesAsync());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}