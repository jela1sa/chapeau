using Chapeau.Models;

namespace Chapeau.Repositories
{
    public interface IBestellingRepository
    {
        List<Bestelling> GetRunningOrders();
        Bestelling GetbestellingByTafel(int Tafel_ID);
    }
}
