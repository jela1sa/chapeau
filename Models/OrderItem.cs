using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chapeau.Models
{
    public class OrderItem
    {
        

        [Key]
        public int OrderItem_id  { get; set; }

        public int Bestelling_id { get; set; }

        public int Item_id { get; set; }

        public int Aantal { get; set; }

      
        public virtual Order Bestelling { get; set; }

        public virtual MenuItem MenuItem { get; set; }


        public OrderItem(int orderItem_id, int bestelling_id, int item_id, int aantal, Order bestelling, MenuItem menuItem)
        {
            OrderItem_id = orderItem_id;
            Bestelling_id = bestelling_id;
            Item_id = item_id;
            Aantal = aantal;
            Bestelling = bestelling;
            MenuItem = menuItem;
        }
    }
}
