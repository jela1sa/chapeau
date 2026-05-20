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
            List<Tafel> tafels = new List<Tafel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT tafel_ID, tafel_nummer, aantal_stoelen, status 
                                 FROM Tafel 
                                 ORDER BY tafel_ID";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int tafel_ID = reader.GetInt32(0);
                    string tafel_nummer = reader.GetString(1);
                    int aantal_stoelen = reader.GetInt32(2);
                    bool status = reader.GetBoolean(3);

                    Tafel tafel = new Tafel(
                        tafel_ID,
                        tafel_nummer,
                        aantal_stoelen,
                        status
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
                string query = @"SELECT tafel_ID, tafel_nummer, aantal_stoelen, status 
                                 FROM Tafel 
                                 WHERE tafel_ID = @tafel_ID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@tafel_ID", tafel_ID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string tafel_nummer = reader.GetString(1);
                    int aantal_stoelen = reader.GetInt32(2);
                    bool status = reader.GetBoolean(3);

                    tafel = new Tafel(
                        id,
                        tafel_nummer,
                        aantal_stoelen,
                        status
                    );
                }
            }

            return tafel;
        }

        public void Update(Tafel tafel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
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

                connection.Open();

                int nrOfRowsAffected = command.ExecuteNonQuery();

                if (nrOfRowsAffected != 1)
                {
                    throw new Exception("Update failed");
                }
            }
        }
    }
}