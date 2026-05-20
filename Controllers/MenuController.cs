using Chapeau.Models;
using Chapeau.Repositories;
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
        List<MenuItem> items = _menusRepository
            .GetFilteredMenuItems(cardFilter, categoryFilter);

        return View(items);
    }
}