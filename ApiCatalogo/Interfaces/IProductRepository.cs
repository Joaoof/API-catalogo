using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ApiCatalogo.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetProducts(ProductsParams productsParams);
        IEnumerable<Product> GetProductsCategories(int id);
    }
}
