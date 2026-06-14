using Chapeau.Models;
using Chapeau.Repositories;
using Chapeau.ViewModels;

namespace Chapeau.Services
{
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
                Rekening_ID = model.Rekening_ID,
                Bestelling_ID = model.Bestelling_ID,
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
