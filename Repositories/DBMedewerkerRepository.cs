using Chapeau.Models;
using Microsoft.Data.SqlClient;

namespace Chapeau.Repositories
{
    public class DBMedewerkerRepository: IMedewerkerRepository
    {
        private readonly string _connectionString;


        public DBMedewerkerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ChapeauDatabase");
        }
        public Medewerker? GetByLoginCredentials(string Gebruikersnaam, string Wachtwoord)
        {
            Medewerker? medewerker = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT bediening_ID, naam, rol, gebruikersnaam, wachtwoord FROM Bediening WHERE gebruikersnaam = @gebruikersnaam AND wachtwoord = @wachtwoord";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@gebruikersnaam", Gebruikersnaam);
                command.Parameters.AddWithValue("@wachtwoord", Wachtwoord);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int bediening_ID = reader.GetInt32(0);
                    string naam = reader.GetString(1);
                    string rol = reader.GetString(2);
                    string gebruikersnaam = reader.GetString(3);
                    string wachtwoord = reader.GetString(4);
                    medewerker = new Medewerker(bediening_ID, naam, rol, gebruikersnaam, wachtwoord);
                }
            }
            return medewerker;
        }
        public void Add(Medewerker medewerker)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO Bediening (bediening_ID, naam, rol, gebruikersnaam, wachtwoord)
                    VALUES (@bediening_ID, @naam, @rol, @gebruikersnaam, @wachtwoord);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@bediening_ID", medewerker.bediening_ID);
                    command.Parameters.AddWithValue("@naam", medewerker.naam);
                    command.Parameters.AddWithValue("@rol", medewerker.rol);
                    command.Parameters.AddWithValue("@gebruikersnaam", medewerker.gebruikersnaam);
                    command.Parameters.AddWithValue("@wachtwoord", medewerker.wachtwoord);
                    command.Connection.Open();

                    int nrOfRowsAffected = command.ExecuteNonQuery();
                    if (nrOfRowsAffected != 1)
                    {
                        throw new Exception("Insert failed");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL ERROR: " + ex.Message);
                throw;
            }

        }
        public List<Medewerker> GetAll()
        {
            List<Medewerker> medewerker = new List<Medewerker>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT bediening_ID, naam, rol, gebruikersnaam, wachtwoord FROM Bediening ORDER BY naam";
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int bediening_ID = reader.GetInt32(0);
                    string naam = reader.GetString(1);
                    string rol = reader.GetString(2);
                    string gebruikersnaam = reader.GetString(3);
                    string wachtwoord = reader.GetString(4);
                    Medewerker medewerkers = new Medewerker(bediening_ID, naam, rol, gebruikersnaam, wachtwoord);
                    medewerker.Add(medewerkers);
                }
            }
            return medewerker;
        }


    }
}
