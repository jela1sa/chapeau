using Chapeau.Models;
using Chapeau.ViewModels;

namespace Chapeau.Repositories
{
    public class RekeningRepository : IRekeningRepository
    {
        private readonly string _connectionString;

        public RekeningRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void VoegToe(Rekening rekening)
        {
            rekening.Add(rekening);
            rekening.SaveChanges();
        }

        public void WijzigTafelStatus(int bestellingId, string status)
        {
            var bestelling = _context.Bestellingen
                .Include(b => b.Tafel)
                .FirstOrDefault(b => b.Bestelling_ID == bestellingId);

            if (bestelling != null)
            {
                bestelling.Tafel.Status = status;
                _context.SaveChanges();
            }
        }
}

// Services/IRekeningService.cs
public interface IRekeningService
    {
        void VerwerkBetaling(RekeningViewModel model);
    }

    // Services/RekeningService.cs
    public class RekeningService : IRekeningService
    {
        private readonly IRekeningRepository _repository;
        private const decimal BTW_PERCENTAGE = 0.21m;

        public RekeningService(IRekeningRepository repository)
        {
            _repository = repository;
        }

        public void VerwerkBetaling(RekeningViewModel model)
        {
            decimal btw = model.TotaalBedrag - (model.TotaalBedrag / (1 + BTW_PERCENTAGE));

            var rekening = new Rekening
            {
                Bestelling_ID = model.Bestelling_ID,
                Rekening_ID = model.Bestelling_ID,
                TotaalBedrag = model.TotaalBedrag,
                Fooi = model.Fooi,
                BtwBedrag = btw,
                Betaalwijze = model.Betaalwijze,
                Feedback = model.Feedback,
                Datum = DateTime.Now
            };

            _repository.VoegToe(rekening);
            _repository.WijzigTafelStatus(model.Bestelling_ID, "vrij");
        }
    }
}
