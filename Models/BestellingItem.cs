using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chapeau.Models
{
    public class BestellingItem
    {
        

        [Key]
        public int BestellingItem_id  { get; set; }

        public int Bestelling_id { get; set; }

        public int Item_id { get; set; }

        public int Aantal { get; set; }

      
        public virtual Bestelling Bestelling { get; set; }

        public virtual MenuItem MenuItem { get; set; }

        public BestellingItem()
        {

        }


        public BestellingItem(int BestellingItem_id, int bestelling_id, int item_id, int aantal, Bestelling bestelling, MenuItem menuItem)
        {
            BestellingItem_id = BestellingItem_id;
            Bestelling_id = bestelling_id;
            Item_id = item_id;
            Aantal = aantal;
            Bestelling = bestelling;
            MenuItem = menuItem;
        }
    }
}
