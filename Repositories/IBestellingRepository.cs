using Chapeau.Models;

namespace Chapeau.Repositories
{
    public interface IBestellingRepository
    {
        List<Bestelling> GetRunningOrders();
        Bestelling GetOrderByTafel(int Tafel_ID);
    }
}
