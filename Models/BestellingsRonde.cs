namespace Chapeau.Models
{
    public class BestellingsRonde
    {
        public int BestellingsRonde_ID { get; set; }
        public string Item_ID { get; set; }
        public int Aantal { get; set; }
        public string Opmerkingen { get; set; }
        public string BestellingsRonde_Status { get; set; }
        public MenuItem MenuItem { get; set; }
        public BestellingsRonde()
        {
        }
        public BestellingsRonde(int bestellingsRonde_ID, string item_ID, int aantal, string opmerkingen, string bestellingsRonde_Status, 
            MenuItem menuItem)
        {
            BestellingsRonde_ID = bestellingsRonde_ID;
            Item_ID = item_ID;
            Aantal = aantal;
            Opmerkingen = opmerkingen;
            BestellingsRonde_Status = bestellingsRonde_Status;
            MenuItem = menuItem;
        }
    }
}
