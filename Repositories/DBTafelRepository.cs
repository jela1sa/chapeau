using Chapeau.Models;
using Microsoft.Data.SqlClient;

namespace Chapeau.Repositories
{
    public class DBTafelRepository : ITafelRepository
    {
        private readonly string _connectionString;


        public DBTafelRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ChapeauDatabase");
        }
        public List<Tafel> GetAll()
        {
            List<Tafel> Tafel = new List<Tafel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT tafel_ID, tafel_nummer, aantal_stoelen, Bestelling_Status FROM Tafel ORDER BY tafel_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int tafel_ID = reader.GetInt32(0);
                    string tafel_nummer = reader.GetString(1);
                    int aantal_stoelen = reader.GetInt32(2);
                    bool Bestelling_Status = reader.GetBoolean(3);
                    Tafel Tafels = new Tafel(tafel_ID, tafel_nummer, aantal_stoelen, Bestelling_Status);
                    Tafel.Add(Tafels);
                }
            }
            return Tafel;
        }

        public void Update(Tafel Tafel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Tafel SET tafel_ID = @tafel_ID, tafel_nummer = @tafel_nummer, aantal_stoelen = @aantal_stoelen, Bestelling_Status = @Bestelling_Status WHERE tafel_ID = @tafel_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tafel_ID", Tafel.Tafel_ID);
                command.Parameters.AddWithValue("@tafel_nummer", Tafel.Tafel_Nummer);
                command.Parameters.AddWithValue("@aantal_stoelen", Tafel.Aantal_stoelen);
                command.Parameters.AddWithValue("@Bestelling_Status", Tafel.Bestelling_Status);
                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected != 1)
                {
                    throw new Exception("Update failed");
                }
            }
        }
        public Tafel? GetById(int tafel_ID)
        {
            Tafel? Tafel = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT tafel_ID, tafel_nummer, aantal_stoelen, Bestelling_Status FROM Tafel WHERE tafel_ID = @tafel_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tafel_ID", tafel_ID);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int Tafel_ID = reader.GetInt32(0);
                    string tafel_nummer = reader.GetString(1);
                    int aantal_stoelen = reader.GetInt32(2);
                    Boolean Bestelling_Status = reader.GetBoolean(3);
                    Tafel = new Tafel(Tafel_ID, tafel_nummer, aantal_stoelen, Bestelling_Status);

                }
            }
            return Tafel;
        }

    }
}
