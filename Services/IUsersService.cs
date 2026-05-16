using Chapeau.Models;

namespace Chapeau.Services
{
    public interface IUsersService
    {
        Medewerker? GetByLoginCredentials(string gebruikersnaam, string wachtwoord);
        public void Add(Medewerker medewerker);
        List<Medewerker> GetAll();


    }

}
