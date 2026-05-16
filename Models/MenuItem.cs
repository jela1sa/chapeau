namespace Chapeau.Models;

public class MenuItem
{
    public string ItemId { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public decimal Prijs { get; set; }
    public string Categorie { get; set; }
    public decimal BTWTarief { get; set; }

    public MenuItem()
    {
    }

    public MenuItem(string itemId, string naam, string beschrijving, decimal prijs, string categorie, decimal btwTarief)
    {
        ItemId = itemId;
        Naam = naam;
        Beschrijving = beschrijving;
        Prijs = prijs;
        Categorie = categorie;
        BTWTarief = btwTarief;
    }
}