namespace Chapeau.Repositorys;
    using Chapeau.Models;   

    public interface IOrderRepository
    {
        Order GetOrderByTable(int tableId);
    }

