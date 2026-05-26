using Chapeau.Models;
namespace Chapeau.Repositories
{
    public interface ITafelRepository
    {
        List<Tafel> GetAll();
        Tafel? GetById(int tafel_ID);
        void Update(Tafel Tafel);
    }
}
