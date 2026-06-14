using Chapeau.Models;
using Chapeau.ViewModels;
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

        //b= bestelling, t= tafel, br= bestellingsronde, mi= menuitem,
        public List<Bestelling> GetRunningOrders()
        {
            List<Bestelling> bestellingen = new List<Bestelling>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT b.bestelling_ID, b.tafel_ID, t.tafel_nummer, b.datum_tijd, b.bestelling_Status, b.bestelling_status,
                 b.tijdstip_opgegeven, br.bestellingsronde_ID, br.item_ID, br.aantal, br.opmerkingen, br.bestellingsronde_status, 
                 mi.naam, mi.categorie
                  FROM Bestelling b 
                  JOIN Tafel t ON b.tafel_ID = t.tafel_ID 
                  JOIN bestellingsRonde br ON b.bestelling_ID = br.bestelling_ID
                  JOIN MenuItem mi ON br.item_ID = mi.item_ID
                   WHERE b.bestelling_Status IN ('besteld', 'in_bereiding') 
                   ORDER BY b.datum_tijd ASC; ";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int bestellingId = reader.GetInt32(0);

                    Bestelling bestaandeBestelling =
                        bestellingen.FirstOrDefault(b => b.Bestelling_ID == bestellingId);

                    if (bestaandeBestelling == null)
                    {
                        DateTime datumTijd = reader.GetDateTime(3);

                        int wachttijd = 0;

                        if (DateTime.Now > datumTijd)
                        {
                            wachttijd = (int)(DateTime.Now - datumTijd).TotalMinutes;
                        }

                        bestaandeBestelling = new Bestelling()
                        {
                            Bestelling_ID = reader.GetInt32(0),
                            Tafel_ID = reader.GetInt32(1),
                            TafelNummer = reader.GetString(2),
                            Datum_Tijd = datumTijd,
                            Bestelling_Status = reader.GetString(4),
                            Tijdstip_Opgegeven = TimeSpan.Parse(reader["tijdstip_opgegeven"].ToString()),
                            Wachttijd = wachttijd,
                            BestellingsRonde = new List<BestellingsRonde>()
                        };

                        bestellingen.Add(bestaandeBestelling);
                    }

                    BestellingsRonde ronde = new BestellingsRonde()
                    {
                        BestellingsRonde_ID = Convert.ToInt32(reader["bestellingsronde_ID"]),
                        Item_ID = reader["item_ID"].ToString(),
                        Aantal = Convert.ToInt32(reader["aantal"]),
                        Opmerkingen = reader["opmerkingen"]?.ToString(),
                        BestellingsRonde_Status = reader["bestellingsronde_status"].ToString(),
                        MenuItem = new MenuItem(reader["item_ID"].ToString(),reader["naam"].ToString(), "", 0, 
                        reader["categorie"].ToString(),0,null)
                    };

                    bestaandeBestelling.BestellingsRonde.Add(ronde);
                }
            }
            return bestellingen;
        }

        //b= bestelling, t= tafel, br= bestellingsronde, mi= menuitem, 
        public List<Bestelling> GetFinishedOrders()
        {
            List<Bestelling> bestellingen = new List<Bestelling>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT b.bestelling_ID,b.tafel_ID,t.tafel_nummer, b.datum_tijd, b.bestelling_status
              FROM Bestelling b
              JOIN Tafel t ON b.tafel_ID = t.tafel_ID
              WHERE b.bestelling_status IN ('gereed','geserveerd') 
              ORDER BY b.datum_tijd DESC";

                SqlCommand command = new SqlCommand(query, connection);


                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bestellingen.Add(new Bestelling
                    {
                        Bestelling_ID = reader.GetInt32(0),
                        Tafel_ID = reader.GetInt32(1),
                        TafelNummer = reader.GetString(2),
                        Datum_Tijd = reader.GetDateTime(3),
                        Bestelling_Status = reader.GetString(4)
                    });
                }
            }

            return bestellingen;
        }



        public List<Bestelling> GetKitchenOrders()
        {
            return GetFilteredOrders("keuken");
        }

        public List<Bestelling> GetBarOrders()
        {
            return GetFilteredOrders("bar");
        }

        private List<Bestelling> GetFilteredOrders(string locatie)
        {
            List<Bestelling> bestellingen = new();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @" SELECT b.bestelling_ID, b.tafel_ID, t.tafel_nummer, b.datum_tijd, b.bestelling_Status, b.tijdstip_opgegeven,
               br.bestellingsronde_ID, br.item_ID, br.aantal, br.opmerkingen, br.bestellingsronde_status, mi.naam, mi.categorie, v.locatie
               FROM Bestelling b
               JOIN Tafel t ON b.tafel_ID = t.tafel_ID
               JOIN BestellingsRonde br ON b.bestelling_ID = br.bestelling_ID
               JOIN MenuItem mi ON br.item_ID = mi.item_ID
               JOIN Voorraad v ON mi.item_ID = v.item_ID
               WHERE v.locatie = @locatie AND b.bestelling_Status IN ('besteld', 'in_bereiding')
               ORDER BY b.datum_tijd ASC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@locatie", locatie);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);

                    var order = bestellingen.FirstOrDefault(x => x.Bestelling_ID == id);

                    if (order == null)
                    {
                        order = new Bestelling
                        {
                            Bestelling_ID = id,
                            Tafel_ID = reader.GetInt32(1),
                            TafelNummer = reader.GetString(2),
                            Datum_Tijd = reader.GetDateTime(3),
                            Bestelling_Status = reader.GetString(4),
                            Tijdstip_Opgegeven = TimeSpan.Parse(reader["tijdstip_opgegeven"].ToString()),
                            BestellingsRonde = new List<BestellingsRonde>()
                        };

                        bestellingen.Add(order);
                    }

                    order.BestellingsRonde.Add(new BestellingsRonde
                    {
                        BestellingsRonde_ID = Convert.ToInt32(reader["bestellingsronde_ID"]),
                        Item_ID = reader["item_ID"].ToString(),
                        Aantal = Convert.ToInt32(reader["aantal"]),
                        Opmerkingen = reader["opmerkingen"]?.ToString(),
                        BestellingsRonde_Status = reader["bestellingsronde_status"].ToString(),
                        MenuItem = new MenuItem(
                            reader["item_ID"].ToString(),
                            reader["naam"].ToString(),
                            "",
                            0,
                            reader["categorie"].ToString(),
                            0,
                            null
                        )
                    });
                }
            }

            return bestellingen;
        }



        public void UpdateOrderStatus(int bestellingId, string status)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Bestelling SET bestelling_status = @status WHERE bestelling_ID = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@id", bestellingId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //b= bestelling, t= tafel, br= bestellingsronde, mi= menuitem,
        public void UpdateCourseStatus(int bestellingId, string naam, string status)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE br SET br.bestellingsronde_status = @status 
                FROM bestellingsRonde br
               INNER JOIN MenuItem mi ON br.item_ID = mi.item_ID
               WHERE br.bestelling_ID = @bestellingId AND mi.naam = @naam";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@bestellingId", bestellingId);
                command.Parameters.AddWithValue("@naam", naam);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void UpdateItemStatus(int bestellingsRondeId, string status)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE BestellingsRonde SET bestellingsronde_status = @status WHERE bestellingsronde_ID = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@id", bestellingsRondeId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

       

       


        public int CreateBestelling(int tafelId, int bedieningId) 
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string getIdQuery = @"SELECT ISNULL(MAX(bestelling_ID),0) + 1 FROM Bestelling";

            SqlCommand getIdCommand =new SqlCommand(getIdQuery, connection);

            int nieuweBestellingId = (int)getIdCommand.ExecuteScalar();

            string insertQuery = @"INSERT INTO Bestelling ( bestelling_ID, tafel_ID, bediening_ID,  datum_tijd,  bestelling_status,
            tijdstip_opgegeven, drink_status)
        VALUES (@bestellingId, @tafelId, @bedieningId,  GETDATE(), 'besteld',CAST(GETDATE() AS TIME),  'nog_niet_besteld'  )";

            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

            insertCommand.Parameters.AddWithValue("@bestellingId", nieuweBestellingId);
            insertCommand.Parameters.AddWithValue("@tafelId", tafelId);
            insertCommand.Parameters.AddWithValue("@bedieningId", bedieningId);

            insertCommand.ExecuteNonQuery();

            return nieuweBestellingId;
        }

        public void AddItemToOrder(int bestellingId, string itemId, int aantal) 

        {
            using SqlConnection connection = new SqlConnection(_connectionString);

        string query = @" INSERT INTO BestellingsRonde(bestelling_ID, item_ID, aantal,  opmerkingen, bestellingsronde_status)
        VALUES
        (@bestellingId, @itemId,  @aantal, '',  'besteld' )";

            SqlCommand command =
                new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@bestellingId", bestellingId);
            command.Parameters.AddWithValue("@itemId", itemId);
            command.Parameters.AddWithValue("@aantal", aantal);

            connection.Open();
            command.ExecuteNonQuery();
        }




        public Bestelling GetbestellingByTafel(int Tafel_ID)
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
    AND b.bestelling_status = 'besteld'
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
                                    Bediening_ID = Convert.ToInt32(reader["bediening_id"]),
                                    Datum_Tijd = Convert.ToDateTime(reader["datum_tijd"]),
                                    Bestelling_Status = reader["Bestelling_Status"].ToString(),
                                    Tijdstip_Opgenomen = TimeOnly.FromDateTime(Convert.ToDateTime(reader["tijdstip_opgegeven"])),
                                    BestellingItems = BestellingItems
                                };
                            }

                            MenuItem menuItem = new MenuItem(
                                reader["item_id"].ToString(),
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
