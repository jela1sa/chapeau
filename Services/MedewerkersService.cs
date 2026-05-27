using Chapeau.Models;
using Chapeau.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Chapeau.Services
{
    public class MedewerkersService: IMedewerkersService
    {
        private readonly IMedewerkerRepository _medewerkerRepository;
        public MedewerkersService(IMedewerkerRepository medewerkerRepository)
        {
            _medewerkerRepository = medewerkerRepository;
        }
        public Medewerker? GetByLoginCredentials(string gebruikersnaam, string wachtwoord)
        {
           return _medewerkerRepository.GetByLoginCredentials(gebruikersnaam, HashPassword(wachtwoord));
        }

        public List<Medewerker> GetAll()
        {
            return _medewerkerRepository.GetAll();
        }
        private string HashPassword(string wachtwoord)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(wachtwoord));
                return Convert.ToBase64String(hashBytes);
            }
        }
        public void Add(Medewerker medewerker)
        {
            Medewerker copyMedewerker = new Medewerker(medewerker);
            copyMedewerker.wachtwoord = HashPassword(medewerker.wachtwoord);
            _medewerkerRepository.Add(copyMedewerker);
            /*if (Medewerker.bediening_ID != copyMedewerker.bediening_ID)
                Medewerker.bediening_ID = copyMedewerker.bediening_ID;*/
        }

    }
}
