using Chapeau.Models;
using Chapeau.Repositories;
using Chapeau.ViewModels;

namespace Chapeau.Services
{
    public class BestellingService : IBestellingService
    {
        private readonly IBestellingRepository _repository;
        private readonly DBDummyRep _dummyRep;


        public BestellingService(IBestellingRepository repository, DBDummyRep dummyRep)
        {
            _repository = repository;
            _dummyRep = dummyRep;
        }

        

        public List<RunningOrderViewModel> GetRunningOrders(string type)
        {
            var orders = _dummyRep.GetDummyBestellingen();

            
            foreach (var order in orders)
            {
                order.Wachttijd =
                    (int)(DateTime.Now - order.Datum_Tijd).TotalMinutes;
            }

            var result = orders.Select(order =>
            {
           
                var kitchenItems = order.BestellingsRonde
                    .Where(r =>
                        r.MenuItem?.Categorie == "Voorgerecht" ||
                        r.MenuItem?.Categorie == "Hoofdgerecht" ||
                        r.MenuItem?.Categorie == "Nagerecht")
                    .ToList();

                
                var barItems = order.BestellingsRonde
                    .Where(r =>
                        r.MenuItem?.Categorie == "Drank")
                    .ToList();

                return new RunningOrderViewModel
                {
                    Bestelling = order,

                    KitchenItems = kitchenItems.Any()
                        ? kitchenItems
                            .GroupBy(x => x.MenuItem.Categorie)
                            .Select(g => new CourseGroepViewModel
                            {
                                Naam = g.Key,
                                Status = "besteld",
                                Items = g.ToList()
                            })
                            .ToList()
                        : new List<CourseGroepViewModel>(),

                  
                    BarItems = barItems.Any()
                        ? barItems
                            .GroupBy(x => x.MenuItem.Categorie)
                            .Select(g => new CourseGroepViewModel
                            {
                                Naam = "Bar",
                                Status = "besteld",
                                Items = g.ToList()
                            })
                            .ToList()
                        : new List<CourseGroepViewModel>()
                };
            }).ToList();

            

            if (type == "kitchen")
                return result.Where(r => r.KitchenItems.Any()).ToList();

            if (type == "bar")
                return result.Where(r => r.BarItems.Any()).ToList();

            return result;
        }


        public void UpdateOrderStatus(int bestellingId, string status)
            => _repository.UpdateOrderStatus(bestellingId, status);

        public void UpdateItemStatus(int bestellingsRondeId, string status)
            => _repository.UpdateItemStatus(bestellingsRondeId, status);

        public void UpdateCourseStatus(int bestellingId, string naam, string status)
            => _repository.UpdateCourseStatus(bestellingId, naam, status);


        public List<Bestelling> GetFinishedOrders()
            => _repository.GetFinishedOrders();

        public BetalingViewModel GetBetalingDetails(int Tafel_ID)
        {
            var order = _repository.GetbestellingByTafel(Tafel_ID);

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


        public int CreateBestelling(int tafelId, int bedieningId)
            => _repository.CreateBestelling(tafelId, bedieningId);


        public void AddItemToOrder(int bestellingId, string itemId, int aantal)
            => _repository.AddItemToOrder(bestellingId, itemId, aantal);
    }
}

