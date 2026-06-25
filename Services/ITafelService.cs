using Chapeau.Models;

namespace Chapeau.Services
{
    public interface ITafelService
    {
        List<Tafel> GetAll();
        Tafel? GetById(int tafel_ID);
        void Update(Tafel Tafel);
    }
}

