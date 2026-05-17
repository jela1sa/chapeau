using Chapeau.Repositorys;
using Chapeau.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Chapeau.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IOrderService _service;

        public PaymentController(IOrderService service)
        {
            _service = service;
        }

        public ActionResult Index(int tableId)
        {
            var model = _service.GetPaymentDetails(tableId);

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
