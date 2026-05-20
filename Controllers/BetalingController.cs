using Chapeau.Repositories;
using Chapeau.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Chapeau.Controllers
{
    [Authorize]
    public class BetalingController : Controller
    {
        private readonly IBestellingService _service;

        public BetalingController(IBestellingService service)
        {
            _service = service;
        }

        public ActionResult Index(int Tafel_ID)
        {
            var model = _service.GetBetalingDetails(Tafel_ID);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}
