namespace Chapeau.Models
{
    public class Rekening
    {
        public int Rekening_ID { get; set;  }

        public int Bestelling_ID { get; set; }

        public string Feedback { get; set; }

        public DateTime Datum { get; set; }

        public Rekening (int rekening_ID, int bestelling_ID, string feedback, DateTime datum)
        {
            Rekening_ID = rekening_ID;
            Bestelling_ID = bestelling_ID;
            Feedback = feedback;
            Datum = datum;
        }
        
          
        
    }
}
