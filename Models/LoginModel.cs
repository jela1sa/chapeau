namespace Chapeau.Models
{
    public class LoginModel
    {
        public int bediening_ID {  get; set; }
        public string gebruikersnaam { get; set; }
        public string wachtwoord { get; set; }

        public LoginModel()
        {

        }

        public LoginModel(int bediening_ID, string gebruikersnaam, string wachtwoord)
        {
            this.bediening_ID = bediening_ID;
            this.gebruikersnaam = gebruikersnaam;
            this.wachtwoord = wachtwoord;
        }

    }
}
