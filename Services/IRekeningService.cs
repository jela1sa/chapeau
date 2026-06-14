using Chapeau.ViewModels;

namespace Chapeau.Services
{
    public interface IRekeningService
    {
        void VerwerkBetaling(RekeningViewModel model);
    }
}
