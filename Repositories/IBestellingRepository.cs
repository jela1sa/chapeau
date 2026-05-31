using Chapeau.Models;

namespace Chapeau.Repositories
{
    public interface IBestellingRepository
    {
        List<Bestelling> GetRunningOrders();
        Bestelling GetOrderByTafel(int Tafel_ID);

        void UpdateOrderStatus(int bestellingId, string status);
        void UpdateItemStatus(int bestellingsRondeId, string status);
        void UpdateCourseStatus(int bestellingId, string categorie, string status);
        List<Bestelling> GetFinishedOrders();
        Bestelling GetbestellingByTafel(int Tafel_ID);
    }
}
