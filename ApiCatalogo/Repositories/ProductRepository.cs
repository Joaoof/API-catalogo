using ApiCatalogo.Context;
using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
 
        }

        public IEnumerable<Product> GetProductsCategories(int id)
        {
           return GetAll().Where(c => c.CategoryId == id);
        }
    }
}
