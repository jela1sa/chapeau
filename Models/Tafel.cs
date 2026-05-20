namespace Chapeau.Models
{
    public class Tafel
    {
        public int Tafel_ID {  get; set; }
        public int Aantal_stoelen  { get; set; }
        public bool Bestelling_Status { get; set; }
        public object Tafel_Nummer { get; set; }
    

        public Tafel()
        {

        }

        public Tafel(int tafel_ID, string tafel_nummer, int aantal_stoelen, bool Bestelling_Status)
        {
            this.Tafel_ID = tafel_ID;
            this.Tafel_Nummer = tafel_nummer;
            this.Aantal_stoelen  = aantal_stoelen;
            this.Bestelling_Status = Bestelling_Status;
        }
    }
}
