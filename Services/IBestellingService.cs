using Chapeau.ViewModels;

namespace Chapeau.Services
{
    public interface IBestellingService
    {
        BetalingViewModel GetBetalingDetails(int Tafel_ID);
    }
}
