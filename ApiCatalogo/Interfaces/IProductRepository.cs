using ApiCatalogo.Models;
using ApiCatalogo.PagedList;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ApiCatalogo.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        PagedList<Product> GetProducts(ProductsParams productsParams);
        IEnumerable<Product> GetProductsCategories(int id);
    }
}
