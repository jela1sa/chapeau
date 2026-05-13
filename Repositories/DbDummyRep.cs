using Microsoft.Data.SqlClient;

namespace Chapeau.Repositories
{
    public class DBDummyRep
    {
        private readonly string _connectionString;

        public DBDummyRep(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ChapeauDatabase");
        }

        public string ConnectionCheck()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    Console.WriteLine("Connection successful!");

                    return "Connection successful!";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection failed: {ex.Message}");

                    return $"Connection failed: {ex.Message}";
                }
            }
        }
    }
}