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

        //add the join of status from bestelling? through tafel_ID
        public List<Tafel> GetAll()
        {
            List<Tafel> tafels = new List<Tafel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT DISTINCT Tafel.tafel_ID, Tafel.tafel_nummer, Tafel.aantal_stoelen, Tafel.status, Bestelling.bestelling_status, Bestelling.drink_status
                                 FROM Tafel JOIN Bestelling ON Tafel.tafel_ID = Bestelling.tafel_ID
                                 ORDER BY Tafel.tafel_ID";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int tafel_ID = reader.GetInt32(0);
                    string tafel_nummer = reader.GetString(1);
                    int aantal_stoelen = reader.GetInt32(2);
                    string status = reader.GetString(3);
                    string bestelling_status = reader.GetString(4);
                    string drink_status = reader.GetString(5);

                    Tafel tafel = new Tafel(
                        tafel_ID,
                        tafel_nummer,
                        aantal_stoelen,
                        status,
                        bestelling_status,
                        drink_status
                    );

                    tafels.Add(tafel);
                }
            }

            return tafels;
        }

        public Tafel? GetById(int tafel_ID)
        {
            Tafel? tafel = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT Tafel.tafel_ID, Tafel.tafel_nummer, Tafel.aantal_stoelen, Tafel.status, Bestelling.bestelling_status, Bestelling.drink_status
                                 FROM Tafel JOIN Bestelling ON Tafel.tafel_ID = Bestelling.tafel_ID
                                 WHERE Tafel.tafel_ID = @tafel_ID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@tafel_ID", tafel_ID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string tafel_nummer = reader.GetString(1);
                    int aantal_stoelen = reader.GetInt32(2);
                    string status = reader.GetString(3);
                    string bestelling_status = reader.GetString(4);
                    string drink_status= reader.GetString(5);

                    tafel = new Tafel(
                        id,
                        tafel_nummer,
                        aantal_stoelen,
                        status,
                        bestelling_status,
                        drink_status
                    );
                }
            }

            return tafel;
        }

        public void Update(Tafel tafel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE Tafel
                         SET tafel_nummer = @tafel_nummer,
                             aantal_stoelen = @aantal_stoelen,
                             status = @status
                         WHERE tafel_ID = @tafel_ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tafel_ID", tafel.Tafel_ID);
                command.Parameters.AddWithValue("@tafel_nummer", tafel.Tafel_Nummer);
                command.Parameters.AddWithValue("@aantal_stoelen", tafel.Aantal_stoelen);
                command.Parameters.AddWithValue("@status", tafel.Status);

                int nrOfRowsAffected = command.ExecuteNonQuery();

                if (nrOfRowsAffected < 1)
                {
                    throw new Exception("Update failed: Tafel not found or not updated.");
                }

                string bestellingQuery = @"UPDATE Bestelling
                                   SET bestelling_status = @bestelling_status,
                                        drink_status = @drink_status
                                   WHERE tafel_ID = @tafel_ID";

                SqlCommand command2 = new SqlCommand(bestellingQuery, connection);
                command2.Parameters.AddWithValue("@tafel_ID", tafel.Tafel_ID);
                command2.Parameters.AddWithValue("@bestelling_status", tafel.bestelling_status);
                command2.Parameters.AddWithValue("@drink_status", tafel.drink_status);

                int nrOfRowsAffected2 = command2.ExecuteNonQuery();

                if (nrOfRowsAffected < 1)
                {
                    throw new Exception("Update failed: Tafel not found or not updated.");
                }

            }
        }
    }
}