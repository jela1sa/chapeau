using Chapeau.Models;

namespace Chapeau.Services
{
    public interface IMedewerkersService
    {
        Medewerker? GetByLoginCredentials(string gebruikersnaam, string wachtwoord);
        public void Add(Medewerker medewerker);
        List<Medewerker> GetAll();


    }

}
