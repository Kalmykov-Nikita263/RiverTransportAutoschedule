using Microsoft.AspNetCore.Mvc;
using RiverTransportAutoschedule.Domain;
using RiverTransportAutoschedule.Domain.Entities;

namespace RiverTransportAutoschedule.Controllers;

public class PortController : Controller
{
    private readonly DataManager _dataManager;

    public PortController(DataManager dataManager)
    {
        _dataManager = dataManager;
    }

    public IActionResult Index()
    {
        return View(_dataManager.Ports.GetAllRiverPortsAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RiverPort riverPort)
    {
        if (ModelState.IsValid)
        {
            await _dataManager.Ports.SaveRiverPortAsync(riverPort);
            return RedirectToAction("Index", "Port");
        }

        return View(riverPort);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var riverPort = await _dataManager.Ports.GetRiverPortByIdAsync(id);
        return View(riverPort);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RiverPort riverPort)
    {
        if (ModelState.IsValid)
        {
            await _dataManager.Ports.SaveRiverPortAsync(riverPort);
            return RedirectToAction("Index", "Port");
        }

        return View(riverPort);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _dataManager.Ports.DeleteRiverPortAsync(id);
        return RedirectToAction("Index", "Port");
    }
}
