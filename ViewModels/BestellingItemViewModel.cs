namespace Chapeau.ViewModels;

public class BestellingItemViewModel
{
    public int MenuItemId { get; set; }

    public string Naam { get; set; } = string.Empty;

    public decimal Prijs { get; set; }

    public int Aantal { get; set; }
}