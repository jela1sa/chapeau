using Chapeau.Models;
using Microsoft.Data.SqlClient;

namespace Chapeau.Repositories
{
    public class BestellingRepository : IBestellingRepository
    {
        private readonly string _connectionString;

        public BestellingRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ChapeauDatabase");
        }

        public List<Bestelling> GetRunningOrders()
        {
            List<Bestelling> bestellingen = new List<Bestelling>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT Bestelling.bestelling_ID, Bestelling.tafel_ID, Tafel.tafel_nummer, Bestelling.datum_tijd, Bestelling.bestelling_status,
                   Bestelling.tijdstip_opgegeven FROM Bestelling JOIN Tafel ON Bestelling.tafel_ID = Tafel.tafel_ID 
                   WHERE Bestelling.bestelling_status IN ('opgenomen', 'in_bereiding') ORDER BY Bestelling.datum_tijd ASC; ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DateTime datumTijd = reader.GetDateTime(3);

                    int wachttijd = 0;

                    if (DateTime.Now > datumTijd)
                    {
                        wachttijd = (int)(DateTime.Now - datumTijd).TotalMinutes;
                    }

                    // int wachttijd = (int)(DateTime.Now - datumTijd).TotalMinutes; // berkening is Nu - gereserveerd datum, maakt het neg ofc 

                    Bestelling bestelling = new Bestelling()
                    {
                        Bestelling_ID = reader.GetInt32(0),
                        Tafel_ID = reader.GetInt32(1),
                        TafelNummer = reader.GetString(2),
                        Datum_Tijd = datumTijd,
                        Bestelling_Status = reader.GetString(4),
                        Tijdstip_Opgegeven = reader.GetTimeSpan(5),
                        Wachttijd = wachttijd
                    };

                    bestellingen.Add(bestelling);
                }
            }

            return bestellingen;
        }
    }
}
