using Chapeau.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chapeau.Controllers
{
    [Authorize]
    public class BestellingController : Controller
    {
        private readonly IBestellingRepository _bestellingRepository;

        public BestellingController(IBestellingRepository bestellingRepository)
        {
            _bestellingRepository = bestellingRepository;
        }

       // public IActionResult RunningOrders()
        //{
        //     var orders = _bestellingRepository.GetRunningOrders();

         //   return View(orders);
        //}

        public IActionResult RunningOrders(string type = "all")
        {
            var orders = type switch
            {
                "kitchen" => _bestellingRepository.GetKitchenOrders(),
                "bar" => _bestellingRepository.GetBarOrders(),
                _ => _bestellingRepository.GetRunningOrders()
            };

            ViewBag.Type = type;
            return View(orders);
        }

       // public IActionResult KitchenOrders()
        //{
         ///   var orders = _bestellingRepository.GetKitchenOrders();
            //return View(orders);
        //}

        //public IActionResult BarOrders()
        //{
          //  var orders = _bestellingRepository.GetBarOrders();
            //return View(orders);
        //}



        [HttpPost]
        public IActionResult UpdateOrderStatus(int bestellingId, string status)
        {
            _bestellingRepository.UpdateOrderStatus(bestellingId, status);

            return RedirectToAction("RunningOrders");
        }

        [HttpPost]
        public IActionResult UpdateItemStatus(int bestellingsRondeId, string status)
        {
            _bestellingRepository.UpdateItemStatus(bestellingsRondeId, status);

            return RedirectToAction("RunningOrders");
        }

        [HttpPost]
        public IActionResult UpdateCourseStatus(int bestellingId, string categorie, string status)
        {
            _bestellingRepository.UpdateCourseStatus(bestellingId,categorie, status);

            return RedirectToAction("RunningOrders");
        }

        public IActionResult FinishedOrders()
        {
            var bestellings = _bestellingRepository.GetFinishedOrders();

            return View(bestellings);
        }

        

    }
}

