using Chapeau.Models;

namespace Chapeau.Repositories
{
    public interface IBestellingRepository
    {
        List<Bestelling> GetRunningOrders();
        Bestelling GetbestellingByTafel(int Tafel_ID);

        void UpdateOrderStatus(int bestellingId, string status);
        void UpdateItemStatus(int bestellingsRondeId, string status);
        void UpdateCourseStatus(int bestellingId, string categorie, string status);
        List<Bestelling> GetFinishedOrders();
        List<Bestelling> GetKitchenOrders();
        List<Bestelling> GetBarOrders();
        int CreateBestelling(int tafelId, int bedieningId); // Voor Mayowa

        void AddItemToOrder(int bestellingId, string itemId, int aantal); // Voor Mayowa

    }
}
