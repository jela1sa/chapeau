using Chapeau.Models;
using Chapeau.Repositories;
using Chapeau.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chapeau.Controllers
{
    [Authorize]
    public class TableController : Controller
    {
        private readonly ITablesRepository _tablesRepository;
        public TableController(ITablesRepository tablesRepository)
        {
            _tablesRepository = tablesRepository;
        }
        public IActionResult Index()
        {
            List<Table> tables = _tablesRepository.GetAll();
            return View(tables);
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
            Table? table = _tablesRepository.GetById((int)id);
            return View(table);
        }
        [HttpPost]
        public ActionResult Edit(Table table)
        {
            try
            {
                //update user via repository
                _tablesRepository.Update(table);
                //go back to users list (via index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(table);
            }
        }
    }
}
