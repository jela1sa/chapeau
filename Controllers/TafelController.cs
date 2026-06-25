using Chapeau.Models;
using Chapeau.Repositories;
using Chapeau.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chapeau.Controllers
{
    [Authorize]
    public class TafelController : Controller
    {
        private readonly ITafelService _TafelService;
        public TafelController(ITafelService TafelService)
        {
            _TafelService = TafelService;
        }
        public IActionResult Index()
        {
            List<Tafel> Tafels = _TafelService.GetAll();
            return View(Tafels);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Tafel? Tafel = _TafelService.GetById((int)id);
            return View(Tafel);
        }
        [HttpPost]
        public ActionResult Edit(Tafel Tafel)
        {
            try
            {
                _TafelService.Update(Tafel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(Tafel);
            }
        }

       
        
    }
}
