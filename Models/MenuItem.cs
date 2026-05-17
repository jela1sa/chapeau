using System.ComponentModel.DataAnnotations;

namespace Chapeau.Models
{
    public class MenuItem
    {
       

        [Key]
        public int Item_id { get; set; }

        public string Naam { get; set; }

        public string Beschrijving { get; set; }

        public decimal Prijs { get; set; }

        public string Categorie { get; set; }

        public decimal Btw_tarief { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }


        public MenuItem(int item_id, string naam, string beschrijving, decimal prijs, string categorie, decimal btw_tarief, ICollection<OrderItem> orderItems)
        {
            Item_id = item_id;
            Naam = naam;
            Beschrijving = beschrijving;
            Prijs = prijs;
            Categorie = categorie;
            Btw_tarief = btw_tarief;
            OrderItems = orderItems;
        }
    }

}
