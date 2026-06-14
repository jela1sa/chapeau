namespace Chapeau.Models
{
    public class Rekening
    {
               public int Rekening_ID { get; set; }
        public int Bestelling_ID { get; set; }
        public decimal TotaalBedrag { get; set; }
        public decimal Fooi { get; set; }
        public decimal BtwBedrag { get; set; }
        public string Betaalwijze { get; set; }
        public string Feedback { get; set; }
        public DateTime Datum { get; set; }

        public Bestelling Bestelling { get; set; }

        public Rekening()
        {
        }


        public Rekening(int rekening_ID, int bestelling_ID, decimal totaalBedrag, decimal fooi, decimal btwBedrag, string betaalwijze, string feedback, DateTime datum, Bestelling bestelling)
        {
            Rekening_ID = rekening_ID;
            Bestelling_ID = bestelling_ID;
            TotaalBedrag = totaalBedrag;
            Fooi = fooi;
            BtwBedrag = btwBedrag;
            Betaalwijze = betaalwijze;
            Feedback = feedback;
            Datum = datum;
            Bestelling = bestelling;
        }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
