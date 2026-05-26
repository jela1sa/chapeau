using Chapeau.Models;

namespace Chapeau.ViewModels;

public class MenuItemStockViewModel
{
    public MenuItem MenuItem { get; set; }
    public Voorraad Voorraad { get; set; }

    public bool IsOutOfStock => Voorraad == null || Voorraad.Hoeveelheid == 0;

    public bool IsAlmostOutOfStock =>
        Voorraad != null && Voorraad.Hoeveelheid > 0 && Voorraad.Hoeveelheid <= 10;
}