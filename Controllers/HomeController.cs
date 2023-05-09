using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiverTransportAutoschedule.Domain.Repository.Abstractions;
using RiverTransportAutoschedule.Models;
using System.Diagnostics;

namespace RiverTransportAutoschedule.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IRiverTransportRepository _transportRepository;

    public HomeController(IRiverTransportRepository transportRepository)
    {
        _transportRepository = transportRepository;
    }

    public IActionResult Index()
    {
        return View(_transportRepository.GetAllRiverTransportsAsync());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}