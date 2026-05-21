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
                string query = @"SELECT Bestelling.bestelling_ID, Bestelling.tafel_ID, Tafel.tafel_nummer, Bestelling.datum_tijd, Bestelling.bestelling_Status,
                   Bestelling.tijdstip_opgegeven FROM Bestelling JOIN Tafel ON Bestelling.tafel_ID = Tafel.tafel_ID 
                   WHERE Bestelling.bestelling_Status IN ('opgenomen', 'in_bereiding') ORDER BY Bestelling.datum_tijd ASC; ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DateTime Datum_Tijd = reader.GetDateTime(3);

                    int wachttijd = 0;

                    if (DateTime.Now > Datum_Tijd)
                    {
                        wachttijd = (int)(DateTime.Now - Datum_Tijd).TotalMinutes;
                    }

                    // int wachttijd = (int)(DateTime.Now - Datum_Tijd).TotalMinutes; // berkening is Nu - gereserveerd datum, maakt het neg ofc 

                    Bestelling bestelling = new Bestelling()
                    {
                        Bestelling_ID = reader.GetInt32(0),
                        Tafel_ID = reader.GetInt32(1),
                        TafelNummer = reader.GetString(2),
                        Datum_Tijd = Datum_Tijd,
                        Bestelling_Status = reader.GetString(4),
                        Tijdstip_Opgegeven = reader.GetTimeSpan(5),
                        Wachttijd = wachttijd
                    };

                    bestellingen.Add(bestelling);
                }
            }

            return bestellingen;
        }
        
        public Bestelling GetOrderByTafel(int Tafel_ID)
        {
            Bestelling order = null;
            List<BestellingItem> BestellingItems = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"
    SELECT 
        b.bestelling_id,
        b.tafel_id,
        b.bediening_id,
        b.datum_tijd,
        b.bestelling_status,
        b.tijdstip_opgegeven,

        oi.order_item_id,
        oi.aantal,

        mi.item_id,
        mi.naam,
        mi.beschrijving,
        mi.prijs,
        mi.categorie,
        mi.btw_tarief

    FROM BESTELLING b

    INNER JOIN ORDER_ITEM oi
        ON b.bestelling_id = oi.bestelling_id

    INNER JOIN MENU_ITEM mi
        ON oi.item_id = mi.item_id

    WHERE b.tafel_id = @Tafel_ID
    AND b.bestelling_status = 'Open'
";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Tafel_ID", Tafel_ID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (order == null)
                            {
                                order = new Bestelling
                                {
                                    Bestelling_ID = Convert.ToInt32(reader["bestelling_id"]),
                                    Tafel_ID = Convert.ToInt32(reader["tafel_id"]),
                                    Bediending_ID = Convert.ToInt32(reader["bediening_id"]),
                                    Datum_Tijd = Convert.ToDateTime(reader["datum_tijd"]),
                                    Bestelling_Status = reader["Bestelling_Status"].ToString(),
                                    Tijdstip_Opgenomen = TimeOnly.FromDateTime(Convert.ToDateTime(reader["tijdstip_opgegeven"])),
                                    BestellingItems = BestellingItems
                                };
                            }

                            MenuItem menuItem = new MenuItem(
                                Convert.ToInt32(reader["item_id"]),
                                reader["naam"].ToString(),
                                reader["beschrijving"].ToString(),
                                Convert.ToDecimal(reader["prijs"]),
                                reader["categorie"].ToString(),
                                Convert.ToDecimal(reader["btw_tarief"]),
                                null
                            );

                            BestellingItem BestellingItem = new BestellingItem()
                            {
                                BestellingItem_id = Convert.ToInt32(reader["order_item_id"]),
                                Aantal = Convert.ToInt32(reader["aantal"]),
                                MenuItem = menuItem
                            };

                            BestellingItems.Add(BestellingItem);
                        }
                    }
                }
            }

            return order;
        }
    }
}
