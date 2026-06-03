namespace Chapeau.ViewModels;

public class BestellingOpnemenViewModel
{
    public int OrderId { get; set; }
    
    public int TafelNummer { get; set; }

    public IEnumerable<MenuItemStockViewModel> MenuItems { get; set; } = new List<MenuItemStockViewModel>();

    public List<BestellingItemViewModel> OrderItems { get; set; } = new();

    public decimal TotalPrice => OrderItems.Sum(x => x.Prijs * x.Aantal);
}