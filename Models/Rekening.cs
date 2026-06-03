namespace Chapeau.Models
{
    public class Rekening
    {
        public int Rekening_ID { get; set;  }

        public int Bestelling_ID { get; set; }

        public int Totaal_Bedrag { get; set; }

        public decimal BTW_6_procent { get; set; }

        public decimal BTW_21_procent { get; set; }

        public decimal fooi { get; set; }

        public string Betaalwijze { get; set; }

        public string Status { get; set; }

        public DateTime Datum { get; set; }

        public Rekening(int rekening_ID, int bestelling_ID, int totaal_Bedrag, decimal bTW_6_procent, decimal bTW_21_procent, decimal fooi, string betaalwijze, string status, DateTime datum)
        {
            Rekening_ID = rekening_ID;
            Bestelling_ID = bestelling_ID;
            Totaal_Bedrag = totaal_Bedrag;
            BTW_6_procent = bTW_6_procent;
            BTW_21_procent = bTW_21_procent;
            this.fooi = fooi;
            Betaalwijze = betaalwijze;
            Status = status;
            Datum = datum;
        }

        }
}
