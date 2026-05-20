namespace Chapeau.Models
{
    public class Tafel
    {
        public int Tafel_ID { get; set; }
        public string Tafel_Nummer { get; set; }
        public int Aantal_stoelen { get; set; }
        public bool Status { get; set; }

        public Tafel()
        {

        }

        public Tafel(int tafel_ID, string tafel_nummer, int aantal_stoelen, bool status)
        {
            Tafel_ID = tafel_ID;
            Tafel_Nummer = tafel_nummer;
            Aantal_stoelen = aantal_stoelen;
            Status = status;
        }
    }
}