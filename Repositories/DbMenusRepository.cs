using Chapeau.Models;
using Microsoft.Data.SqlClient;

namespace Chapeau.Repositories;

public class DbMenusRepository : IMenusRepository
{

    private readonly string _connectionString;
    
    public DbMenusRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ChapeauDatabase");
    }

    private MenuItem readMenuItem(SqlDataReader reader)
    {
        string id = (string)reader["item_ID"];
        string naam = (string)reader["naam"];
        string beschrijving = (string)reader["beschrijving"];
        decimal prijs = (decimal)reader["prijs"];
        string categorie = (string)reader["categorie"];
        decimal btwTarief = (decimal)reader["BTW_Tarief"];

        return new MenuItem(id, naam, beschrijving, prijs, categorie, btwTarief);
    }
    
    public List<MenuItem> GetAll()
    {
        List<MenuItem> menuItems = new List<MenuItem>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * From MenuItem";
            SqlCommand command = new SqlCommand(query, connection);
            command.Connection.Open();
            
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    MenuItem menuItem = readMenuItem(reader);
                    menuItems.Add(menuItem);
                }
            }
            
        }

        return menuItems;
    }
    
    public List<MenuItem> GetFilteredMenuItems(string cardFilter, string categoryFilter)
    {
        string query = @"
        SELECT *
        FROM MenuItem
        WHERE
        (
            @cardFilter = 'all'
            OR Categorie LIKE '%' + @cardFilter + '%'
        )
        AND
        (
            @categoryFilter = 'all'
            OR Naam LIKE '%' + @categoryFilter + '%'
        )";

        List<MenuItem> menuItems = new List<MenuItem>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@cardFilter", cardFilter);
            cmd.Parameters.AddWithValue("@categoryFilter", categoryFilter);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                MenuItem menuItem = readMenuItem(reader);
                menuItems.Add(menuItem);
            }
        }

        return menuItems;
    }

    public MenuItem? GetById(int menuItemId)
    {
        throw new NotImplementedException();
    }

    public void Add(MenuItem menuItem)
    {
        throw new NotImplementedException();
    }

    public void Update(MenuItem menuItem)
    {
        throw new NotImplementedException();
    }

    public void Delete(MenuItem menuItem)
    {
        throw new NotImplementedException();
    }
}