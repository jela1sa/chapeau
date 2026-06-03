using Chapeau.Models;
using Chapeau.ViewModels;

namespace Chapeau.Repositories
{
    public interface IBestellingRepository
    {
        List<Bestelling> GetRunningOrders();
        Bestelling GetOrderByTafel(int Tafel_ID);
        List<BestellingItemViewModel> GetOrderItems(int orderId);
        void AddItemToOrder(int orderId, int menuItemId);
        void SubmitOrder(int orderId);
        IEnumerable<MenuItemStockViewModel> GetMenuItems(string cardFilter, string categoryFilter);
    }
}
