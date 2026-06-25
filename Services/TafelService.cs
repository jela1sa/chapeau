using Chapeau.Models;
using Chapeau.Repositories;

namespace Chapeau.Services
{
    public class TafelService : ITafelService
    {
        private readonly ITafelRepository _TafelsRepository;

        public TafelService(ITafelRepository TafelsRepository)
        {
            _TafelsRepository = TafelsRepository;
        }
        public List<Tafel> GetAll ()
        {
            return _TafelsRepository.GetAll();
        }
        public Tafel? GetById(int tafel_ID)
        {
            return _TafelsRepository.GetById(tafel_ID);
        }

        public void Update(Tafel Tafel)
        {
            _TafelsRepository.Update(Tafel);
        }
    }
}
