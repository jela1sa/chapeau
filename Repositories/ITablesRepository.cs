using Chapeau.Models;
namespace Chapeau.Repositories
{
    public interface ITablesRepository
    {
        List<Table> GetAll();
        Table? GetById(int tafel_ID);
        void Update(Table table);
    }
}
