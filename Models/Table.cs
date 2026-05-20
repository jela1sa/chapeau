namespace Chapeau.Models
{
    public class Table
    {
        public int tafel_ID {  get; set; }
        public string tafel_nummer { get; set; }
        public int aantal_stoelen  { get; set; }
        public bool status { get; set; }

        public Table()
        {

        }

        public Table(int tafel_ID, string tafel_nummer, int aantal_stoelen, bool status)
        {
            this.tafel_ID = tafel_ID;
            this.tafel_nummer = tafel_nummer;
            this.aantal_stoelen = aantal_stoelen;
            this.status = status;
        }
    }
}
