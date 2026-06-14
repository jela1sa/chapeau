using Chapeau.Models;
using Chapeau.ViewModels;

namespace Chapeau.Services
{
    public interface IBestellingService
    {
        BetalingViewModel GetBetalingDetails(int Tafel_ID);
        List<RunningOrderViewModel> GetRunningOrders(string type);

        void UpdateOrderStatus(int bestellingId, string status);
        void UpdateItemStatus(int bestellingsRondeId, string status);
        void UpdateCourseStatus(int bestellingId, string naam, string status);

        List<Bestelling> GetFinishedOrders();

        int CreateBestelling(int tafelId, int bedieningId);

        void AddItemToOrder(int bestellingId, string itemId, int aantal);
    }
}
