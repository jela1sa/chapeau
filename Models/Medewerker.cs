namespace Chapeau.Models
{
    public class Medewerker
    {
        public int bediening_ID {  get; set; }
        public string naam {  get; set; }
        public string rol {  get; set; }
        public string gebruikersnaam { get; set; }
        public string wachtwoord { get; set; }

        public Medewerker()
        {

        }


        public Medewerker(int bediening_ID, string naam, string rol, string gebruikersnaam, string wachtwoord)
        {
            this.bediening_ID = bediening_ID;
            this.naam = naam;
            this.rol = rol;
            this.gebruikersnaam = gebruikersnaam;
            this.wachtwoord = wachtwoord;
        }
        public Medewerker(Medewerker other)
        {
            bediening_ID = other.bediening_ID;
            naam = other.naam;
            rol = other.rol;
            gebruikersnaam = other.gebruikersnaam;
            wachtwoord = other.wachtwoord;
        }
    }
}
