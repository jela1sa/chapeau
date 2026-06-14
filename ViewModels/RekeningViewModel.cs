namespace Chapeau.ViewModels
{
    public class RekeningViewModel
    {
        public int Rekening_ID { get; set; }
        public int Bestelling_ID { get; set; }
        public decimal TotaalBedrag { get; set; }
        public decimal Fooi { get; set; }
        public string Betaalwijze { get; set; }
        public string Feedback { get; set; }
    }
}
