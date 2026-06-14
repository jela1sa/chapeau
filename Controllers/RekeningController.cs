using Chapeau.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Chapeau.Services;

namespace Chapeau.Controllers
{
    public class RekeningController : Controller
    {
        private readonly IRekeningService _service;

        public RekeningController(IRekeningService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Afrekenen(int bestellingId)
        {
            var model = new RekeningViewModel
            {
                Bestelling_ID = bestellingId
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Afrekenen(RekeningViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _service.VerwerkBetaling(model);

            TempData["Bevestiging"] = "Betaling succesvol verwerkt. Bestelling afgerond.";
            return RedirectToAction("Bevestiging");
        }

        [HttpGet]
        public IActionResult Bevestiging()
        {
            return View();
        }
    }
}
    
