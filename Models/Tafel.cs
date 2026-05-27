namespace Chapeau.Models
{
    //add the foreign keys or something from bestelling?
    //add the bestelling status
    public class Tafel
    {
        public int Tafel_ID { get; set; }
        public string Tafel_Nummer { get; set; }
        public int Aantal_stoelen { get; set; }
        public string Status { get; set; }
        public string bestelling_status { get; set; }

        public Tafel()
        {

        }

        public Tafel(int tafel_ID, string tafel_nummer, int aantal_stoelen, string status, string bestelling_status)
        {
            Tafel_ID = tafel_ID;
            Tafel_Nummer = tafel_nummer;
            Aantal_stoelen = aantal_stoelen;
            Status = status;
            this.bestelling_status = bestelling_status;
        }
    }
}