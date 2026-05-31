using Chapeau.Models;
using Chapeau.Repositories;
using Chapeau.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Chapeau.Controllers;

public class MenuController : Controller
{
    private IMenusRepository _menusRepository;

    public MenuController(IMenusRepository menusRepository)
    {
        _menusRepository = menusRepository;
    }

    [HttpGet]
    public IActionResult Index(int bestellingId, string cardFilter = "all", string categoryFilter = "all")
    {
        ViewBag.BestellingId = bestellingId;

        var items = _menusRepository.GetMenuWithStock(cardFilter, categoryFilter);

        return View(items);
    }
}