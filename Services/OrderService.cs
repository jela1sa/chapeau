using Chapeau.Repositorys;
using Chapeau.ViewModels;

namespace Chapeau.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public PaymentViewModel GetPaymentDetails(int tableId)
        {
            var order = _repository.GetOrderByTable(tableId);

            if (order == null)
                return null;

            var groupedItems = order.OrderItems
                .GroupBy(x => x.MenuItem.Naam)
                .Select(g =>
                {
                    var first = g.First();

                    int quantity = g.Sum(x => x.Aantal);

                    decimal total = quantity * first.MenuItem.Prijs;

                    return new PaymentItemViewModel
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

            return new PaymentViewModel
            {
                TableId = tableId,
                Items = groupedItems,
                TotalAmount = totalAmount,
                LowVatTotal = lowVat,
                HighVatTotal = highVat
            };
        }
    }
}

