namespace Chapeau.Repositories;
    using Chapeau.Models;   

    public interface IRekeningRepository
    {
    void VoegToe(Rekening rekening);
    void WijzigTafelStatus(int bestellingId, string status);
}

