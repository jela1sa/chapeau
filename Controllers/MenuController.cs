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
    public List<MenuItem> Menu()
    {
        return _menusRepository.GetAll();
    }
}