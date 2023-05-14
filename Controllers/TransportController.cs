using Microsoft.AspNetCore.Mvc;
using RiverTransportAutoschedule.Domain;
using RiverTransportAutoschedule.Domain.Entities;

namespace RiverTransportAutoschedule.Controllers;

public class TransportController : Controller
{
    private readonly DataManager _dataManager;

    public TransportController(DataManager dataManager)
    {
        _dataManager = dataManager;
    }

    public IActionResult Index()
    {
        return View(_dataManager.Transports.GetAllRiverTransportsAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RiverTransport riverTransport)
    {
        if (ModelState.IsValid)
        {
            await _dataManager.Transports.SaveRiverTransportAsync(riverTransport);
            return RedirectToAction("Index", "Transport");
        }

        return View(riverTransport);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var riverPort = await _dataManager.Transports.GetRiverTransportByIdAsync(id);
        return View(riverPort);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RiverTransport riverTransport)
    {
        if (ModelState.IsValid)
        {
            await _dataManager.Transports.SaveRiverTransportAsync(riverTransport);
            return RedirectToAction("Index", "Transport");
        }

        return View(riverTransport);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _dataManager.Transports.DeleteRiverTransportById(id);
        return RedirectToAction("Index", "Transport");
    }
}
