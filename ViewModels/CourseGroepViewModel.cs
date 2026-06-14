using Chapeau.Models;

namespace Chapeau.ViewModels
{
    public class CourseGroepViewModel
    {

        public string Naam { get; set; }
        public string Status { get; set; }
        public List<BestellingsRonde> Items { get; set; }
    }
}
