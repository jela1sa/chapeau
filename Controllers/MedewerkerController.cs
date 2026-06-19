using Chapeau.Models;
using Chapeau.Repositories;
using Chapeau.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Chapeau.Controllers
{
    [Authorize]
    public class MedewerkerController : Controller
    {
        private readonly IMedewerkersService _medewerkersService;
        public MedewerkerController(IMedewerkersService medewerkersService)
        {
            _medewerkersService = medewerkersService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Account()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Medewerker medewerker)
        {
            try
            {
                _medewerkersService.Add(medewerker);
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                return View(medewerker);
            }
        }


    }
}
