namespace Chapeau.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int TafelId { get; set; }

        public int BedieningId { get; set; }

        public DateTime DatumTijd { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public string Status { get; set; }
        public TimeOnly TijdstipOpgenomen { get; set; } = new TimeOnly();

        public Order(int orderId, int tafelId, int bedieningId, DateTime datumTijd)
        {
            OrderId = orderId;
            TafelId = tafelId;
            BedieningId = bedieningId;
            DatumTijd = datumTijd;
        }

        public Order() { }
    }
}
