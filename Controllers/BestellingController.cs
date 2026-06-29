using Chapeau.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chapeau.Controllers
{
    [Authorize]
    public class BestellingController : Controller
    {
        private readonly IBestellingService _bestellingService;

        public BestellingController(IBestellingService bestellingService)
        {
            _bestellingService = bestellingService;
        }

        

        public IActionResult RunningOrders(string type = "all")
        {
            var vm = _bestellingService.GetRunningOrders(type);
            return View(vm);
        }

        

        [HttpPost]
        public IActionResult UpdateOrderStatus(int bestellingId, string status)
        {
            _bestellingService.UpdateOrderStatus(bestellingId, status);
            return RedirectToAction("FinishedOrders");
        }

        [HttpPost]
        public IActionResult UpdateItemStatus(int bestellingsRondeId, string status)
        {
            _bestellingService.UpdateItemStatus(bestellingsRondeId, status);
            return RedirectToAction("RunningOrders");
        }

        [HttpPost]
        public IActionResult UpdateCourseStatus(int bestellingId, string naam, string status)
        {
            _bestellingService.UpdateCourseStatus(bestellingId, naam, status);
            return RedirectToAction("RunningOrders");
        }


        public IActionResult FinishedOrders()
        {
            var model = _bestellingService.GetFinishedOrders();
            return View(model);
        }


        [HttpPost]
        public IActionResult CreateBestelling(int tafelId)
        {
            int bedieningId = 12; // Ingelogde medewerker ID ophalen

            int bestellingId = _bestellingService.CreateBestelling(tafelId, bedieningId);


            return RedirectToAction("Index", "Menu", new { bestellingId });
        }


        [HttpPost]
        public IActionResult AddItemToOrder(int bestellingId, string itemId, int aantal)
        {
            _bestellingService.AddItemToOrder(bestellingId, itemId, aantal);

            return RedirectToAction("Index", "Menu", new { bestellingId });
        }
    }
}