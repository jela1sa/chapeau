using Microsoft.Data.SqlClient;
using Chapeau.Models;

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



        public List<Bestelling> GetDummyBestellingen()
        {
            return new List<Bestelling>
    {
        // keuken + bar
        new Bestelling
        {
            Bestelling_ID = 1,
            Tafel_ID = 1,
            TafelNummer = "1",
            Datum_Tijd = DateTime.Now.AddMinutes(-25),
            Bestelling_Status = "besteld",
            BestellingsRonde = new List<BestellingsRonde>
            {
                new BestellingsRonde
                {
                    BestellingsRonde_ID = 1,
                    Item_ID = "D1",
                    Aantal = 2,
                    Opmerkingen = "Geen ijs",
                    BestellingsRonde_Status = "besteld",
                    MenuItem = new MenuItem(
                        "D1",
                        "Cola",
                        "",
                        3.50m,
                        "Drank",
                        21,
                        null)
                },

                new BestellingsRonde
                {
                    BestellingsRonde_ID = 2,
                    Item_ID = "V1",
                    Aantal = 1,
                    Opmerkingen = "",
                    BestellingsRonde_Status = "besteld",
                    MenuItem = new MenuItem(
                        "V1",
                        "Voorgerecht Tomatensoep",
                        "",
                        6.50m,
                        "Voorgerecht",
                        9,
                        null)
                }
            }
        },

        // keuken
        new Bestelling
        {
            Bestelling_ID = 2,
            Tafel_ID = 4,
            TafelNummer = "4",
            Datum_Tijd = DateTime.Now.AddMinutes(-15),
            Bestelling_Status = "in_bereiding",
            BestellingsRonde = new List<BestellingsRonde>
            {
                new BestellingsRonde
                {
                    BestellingsRonde_ID = 3,
                    Item_ID = "H1",
                    Aantal = 2,
                    Opmerkingen = "Medium",
                    BestellingsRonde_Status = "in_bereiding",
                    MenuItem = new MenuItem(
                        "H1",
                        "Hoofdgerecht Biefstuk",
                        "",
                        24.50m,
                        "Hoofdgerecht",
                        9,
                        null)
                },

                new BestellingsRonde
                {
                    BestellingsRonde_ID = 4,
                    Item_ID = "N1",
                    Aantal = 2,
                    Opmerkingen = "",
                    BestellingsRonde_Status = "besteld",
                    MenuItem = new MenuItem(
                        "N1",
                        "Nagerecht Cheesecake",
                        "",
                        7.50m,
                        "Nagerecht",
                        9,
                        null)
                }
            }
        },

        //  bar
        new Bestelling
        {
            Bestelling_ID = 3,
            Tafel_ID = 7,
            TafelNummer = "7",
            Datum_Tijd = DateTime.Now.AddMinutes(-5),
            Bestelling_Status = "besteld",
            BestellingsRonde = new List<BestellingsRonde>
            {
                new BestellingsRonde
                {
                    BestellingsRonde_ID = 5,
                    Item_ID = "D2",
                    Aantal = 3,
                    Opmerkingen = "",
                    BestellingsRonde_Status = "besteld",
                    MenuItem = new MenuItem(
                        "D2",
                        "Bier",
                        "",
                        4.00m,
                        "Drank",
                        21,
                        null)
                },

                new BestellingsRonde
                {
                    BestellingsRonde_ID = 6,
                    Item_ID = "D3",
                    Aantal = 1,
                    Opmerkingen = "Met citroen",
                    BestellingsRonde_Status = "besteld",
                    MenuItem = new MenuItem(
                        "D3",
                        "Spa Rood",
                        "",
                        3.00m,
                        "Drank",
                        21,
                        null)
                }
            }
        }
    };
        }
    }
}