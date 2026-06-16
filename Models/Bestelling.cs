using Chapeau.ViewModels;

namespace Chapeau.Models
{
    public class Bestelling
    {
        public int Bestelling_ID { get; set; }
        public int Tafel_ID { get; set; }         
        public string TafelNummer { get; set; }
        public  int Bediening_ID { get; set; }
        public DateTime Datum_Tijd { get; set; }
        public string Bestelling_Status { get; set; }
        public TimeSpan Tijdstip_Opgegeven { get; set; }
        public int Wachttijd { get; set; }
        public virtual ICollection<BestellingItem> BestellingItems { get; set; }
        public TimeOnly Tijdstip_Opgenomen { get; set; } = new TimeOnly();
        public List<BestellingsRonde> BestellingsRonde { get; set; }

        public Bestelling()
        {
        }
        public Bestelling(int bestelling_ID, int tafel_ID, string tafelNummer, int bediening_ID, DateTime datum_Tijd, string bestelling_Status, 
            TimeSpan tijdstip_Opgegeven, int wachttijd)
        {
            Bestelling_ID = bestelling_ID;
            Tafel_ID = tafel_ID;
            TafelNummer = tafelNummer;  
            Bediening_ID = bediening_ID;
            Datum_Tijd = datum_Tijd;
            Bestelling_Status = bestelling_Status;
            Tijdstip_Opgegeven = tijdstip_Opgegeven;
            Wachttijd = wachttijd;
        }
    }
}
