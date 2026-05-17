using Chapeau.ViewModels;

namespace Chapeau.Services
{
    public interface IOrderService
    {
        PaymentViewModel GetPaymentDetails(int tableId);
    }
}
