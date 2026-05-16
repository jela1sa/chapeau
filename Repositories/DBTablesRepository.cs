using Chapeau.Models;
using Microsoft.Data.SqlClient;

namespace Chapeau.Repositories
{
    public class DBTablesRepository : ITablesRepository
    {
        private readonly string _connectionString;


        public DBTablesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ChapeauDatabase");
        }
        public List<Table> GetAll()
        {
            List<Table> table = new List<Table>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT tafel_ID, tafel_nummer, aantal_stoelen, status FROM Tafel ORDER BY tafel_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int tafel_ID = reader.GetInt32(0);
                    string tafel_nummer = reader.GetString(1);
                    int aantal_stoelen = reader.GetInt32(2);
                    bool status = reader.GetBoolean(3);
                    Table tables = new Table(tafel_ID, tafel_nummer, aantal_stoelen, status);
                    table.Add(tables);
                }
            }
            return table;
        }

        public void Update(Table table)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Tafel SET tafel_ID = @tafel_ID, tafel_nummer = @tafel_nummer, aantal_stoelen = @aantal_stoelen, status = @status WHERE tafel_ID = @tafel_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tafel_ID", table.tafel_ID);
                command.Parameters.AddWithValue("@tafel_nummer", table.tafel_nummer);
                command.Parameters.AddWithValue("@aantal_stoelen", table.aantal_stoelen);
                command.Parameters.AddWithValue("@status", table.status);
                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected != 1)
                {
                    throw new Exception("Update failed");
                }
            }
        }
        public Table? GetById(int tafel_ID)
        {
            Table? table = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT tafel_ID, tafel_nummer, aantal_stoelen, status FROM Tafel WHERE tafel_ID = @tafel_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tafel_ID", tafel_ID);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int Tafel_ID = reader.GetInt32(0);
                    string tafel_nummer = reader.GetString(1);
                    int aantal_stoelen = reader.GetInt32(2);
                    Boolean status = reader.GetBoolean(3);
                    table = new Table(Tafel_ID, tafel_nummer, aantal_stoelen, status);

                }
            }
            return table;
        }

    }
}
