namespace Chapeau.Repositories;
    using Chapeau.Models;   

    public interface IRekeningRepository
    {
        Rekening GetbestellingByTafel(int tableId);
    }

