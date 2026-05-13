namespace Chapeau.Models;

public class MenuItem
{
    public int ItemId { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public double Prijs { get; set; }
    public string Categorie { get; set; }
    public string BTW_Tarief { get; set; }

    public MenuItem()
    {
    }

    public MenuItem(int itemId, string naam, string beschrijving, double prijs, string categorie, string btwTarief)
    {
        ItemId = itemId;
        Naam = naam;
        Beschrijving = beschrijving;
        Prijs = prijs;
        Categorie = categorie;
        BTW_Tarief = btwTarief;
    }
}