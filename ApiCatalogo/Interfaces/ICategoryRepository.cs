using ApiCatalogo.Models;

namespace ApiCatalogo.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategoriesProd(int CategoryId);
        IEnumerable<Category> GetCategories();

        Category GetCategory(int id);

        Category Create(Category category);

        Category Update(Category category);

        Category Delete(int id);
    }
}
