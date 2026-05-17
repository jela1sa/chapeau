namespace Chapeau.ViewModels
{
    public class PaymentViewModel
    {
        public int TableId { get; set; }

        public List<PaymentItemViewModel> Items { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal LowVatTotal { get; set; }

        public decimal HighVatTotal { get; set; }
    }

    public class PaymentItemViewModel
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerItem { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal VatRate { get; set; }
    }
}

