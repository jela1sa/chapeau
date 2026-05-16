using Chapeau.Models;

namespace Chapeau.Repositories
{
    public interface IMedewerkerRepository
    {
        Medewerker? GetByLoginCredentials(string gebruikersnaam, string wachtwoord);
        public void Add(Medewerker medewerker);
        List<Medewerker> GetAll();


    }
}
