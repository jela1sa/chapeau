using Chapeau.Models;

namespace Chapeau.Repositories;

public interface MenuRepository
{
    List<MenuItem> GetAll();
    MenuItem? GetById(int menuItemId);
    void Add(MenuItem menuItem);
    void Update(MenuItem menuItem);
    void Delete(MenuItem menuItem);
}