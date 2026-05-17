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
        private readonly IUsersService _usersService;
        public MedewerkerController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public IActionResult Index()
        {
            /*Medewerker? loggedInMedewerker = null;
            string? medewerkerJson = HttpContext.Session.GetString("LoggedInMedewerker");
            if (medewerkerJson != null)
                loggedInMedewerker = JsonSerializer.Deserialize<Medewerker>(medewerkerJson);

            ViewData["LoggedInMedewerker"] = loggedInMedewerker;
            List<Medewerker> medewerker = _usersService.GetAll();*/
            return View(/*medewerker*/);
        }
        public ActionResult Account()
        {
            Medewerker? loggedInMedewerker = HttpContext.Session.GetObject<Medewerker>("LoggedInMedewerker");

            ViewData["LoggedInMedewerker"] = loggedInMedewerker;
            return View(loggedInMedewerker);
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
                //add user via repository
                _usersService.Add(medewerker);
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                //somtehing's wrong, go back to view with user
                return View(medewerker);
            }
        }


    }
}
