using Chapeau.Models;

namespace Chapeau.ViewModels
{
    public class RunningOrderViewModel
    {
        public Bestelling Bestelling { get; set; }

        public List<CourseGroepViewModel> Categories { get; set; }
    }
}
