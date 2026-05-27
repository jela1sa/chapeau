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
        private readonly ITafelRepository _TafelsRepository;
        public TafelController(ITafelRepository TafelsRepository)
        {
            _TafelsRepository = TafelsRepository;
        }
        public IActionResult Index()
        {
            List<Tafel> Tafels = _TafelsRepository.GetAll();
            return View(Tafels);
        }

        /*public IActionResult Edit()
        {
            return View();
        }*/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Tafel? Tafel = _TafelsRepository.GetById((int)id);
            return View(Tafel);
        }
        [HttpPost]
        public ActionResult Edit(Tafel Tafel)
        {
            try
            {
                //update user via repository
                _TafelsRepository.Update(Tafel);
                //go back to users list (via index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(Tafel);
            }
        }
    }
}
