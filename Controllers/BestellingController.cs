using Chapeau.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Chapeau.Controllers
{
    public class BestellingController : Controller
    {
        private readonly IBestellingRepository _bestellingRepository;

        public BestellingController(IBestellingRepository bestellingRepository)
        {
            _bestellingRepository = bestellingRepository;
        }

        public IActionResult RunningOrders()
        {
            var orders = _bestellingRepository.GetRunningOrders();

            return View(orders);
        }
    }
}
