namespace Chapeau.Models;

public class Voorraad
{
    public int VoorraadId { get; set; }
    public string ItemId { get; set; }
    public string Locatie { get; set; }
    public int Hoeveelheid { get; set; }
    public int MinimumNiveau { get; set; }

    public Voorraad()
    {
    }

    public Voorraad(int voorraadId, string itemId, string locatie, int hoeveelheid, int minimumNiveau)
    {
        VoorraadId = voorraadId;
        ItemId = itemId;
        Locatie = locatie;
        Hoeveelheid = hoeveelheid;
        MinimumNiveau = minimumNiveau;
    }
}