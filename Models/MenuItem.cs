using System.ComponentModel.DataAnnotations;

namespace Chapeau.Models
{
    public class MenuItem
    {
       

        [Key]
        public string Item_id { get; set; }

        public string Naam { get; set; }

        public string Beschrijving { get; set; }

        public decimal Prijs { get; set; }

        public string Categorie { get; set; }

        public decimal Btw_tarief { get; set; }

        public virtual ICollection<BestellingItem> BestellingItems { get; set; }


        public MenuItem(string item_id, string naam, string beschrijving, decimal prijs, string categorie, decimal btw_tarief, ICollection<BestellingItem> BestellingItems)
        {
            Item_id = item_id;
            Naam = naam;
            Beschrijving = beschrijving;
            Prijs = prijs;
            Categorie = categorie;
            Btw_tarief = btw_tarief;
            BestellingItems = BestellingItems;
        }
        
        public MenuItem(string item_id, string naam, string beschrijving, decimal prijs, string categorie, decimal btw_tarief)
        {
            Item_id = item_id;
            Naam = naam;
            Beschrijving = beschrijving;
            Prijs = prijs;
            Categorie = categorie;
            Btw_tarief = btw_tarief;
        }

        public string Course
        {
            get
            {
                if (Naam.Contains("Voorgerecht"))
                    return "Voorgerecht";

                if (Naam.Contains("Hoofdgerecht"))
                    return "Hoofdgerecht";

                if (Naam.Contains("Nagerecht"))
                    return "Nagerecht";

                return "Overig";
            }
        }
    }

}
