using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ApiCatalogo.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetProductsCategories(int id);
    }
}
