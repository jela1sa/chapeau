using Chapeau.Repositories;
using Chapeau.ViewModels;

namespace Chapeau.Services
{
    public class BestellingService : IBestellingService
    {
        private readonly IBestellingRepository _repository;

        public BestellingService(IBestellingRepository repository)
        {
            _repository = repository;
        }

        public BetalingViewModel GetBetalingDetails(int Tafel_ID)
        {
            var order = _repository.GetOrderByTafel(Tafel_ID);

            if (order == null)
                return null;

            var groupedItems = order.BestellingItems
                .GroupBy(x => x.MenuItem.Naam)
                .Select(g =>
                {
                    var first = g.First();

                    int quantity = g.Sum(x => x.Aantal);

                    decimal total = quantity * first.MenuItem.Prijs;

                    return new BetalingItemViewModel
                    {
                        Name = first.MenuItem.Naam,
                        Quantity = quantity,
                        PricePerItem = first.MenuItem.Prijs,
                        TotalPrice = total,
                        VatRate = first.MenuItem.Btw_tarief
                    };
                })
                .ToList();

            decimal totalAmount = groupedItems.Sum(x => x.TotalPrice);

            decimal lowVat = groupedItems
                .Where(x => x.VatRate <= 9)
                .Sum(x => x.TotalPrice);

            decimal highVat = groupedItems
                .Where(x => x.VatRate > 9)
                .Sum(x => x.TotalPrice);

            return new BetalingViewModel
            {
                Tafel_ID = Tafel_ID,
                Items = groupedItems,
                TotalAmount = totalAmount,
                LowVatTotal = lowVat,
                HighVatTotal = highVat
            };
        }
    }
}

