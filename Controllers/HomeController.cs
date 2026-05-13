using Chapeau.Models;
using Chapeau.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Chapeau.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBDummyRep _dbDummyRepos;

        public HomeController(ILogger<HomeController> logger, DBDummyRep dbDummyRepos)
        {
            _logger = logger;
            _dbDummyRepos = dbDummyRepos;
        }


        public IActionResult TestConnection()
        {
            _dbDummyRepos.ConnectionCheck();
            return Content("Check je console for results.");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
