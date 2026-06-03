using Chapeau.Models;

namespace Chapeau.ViewModels
{
    public class CategorieGroepViewModel
    {
        public string Categorie { get; set; }
        public string Status { get; set; }  
        public List<BestellingsRonde> Items { get; set; }
    }
}
