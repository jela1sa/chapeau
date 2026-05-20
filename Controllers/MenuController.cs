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
    public IActionResult Index(string cardFilter = "all",
        string categoryFilter = "all")
    {
        List<MenuItemStockViewModel> items = _menusRepository
            .GetMenuWithStock(cardFilter, categoryFilter);

        return View(items);
    }
}