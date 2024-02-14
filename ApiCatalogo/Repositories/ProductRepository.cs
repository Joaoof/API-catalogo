using ApiCatalogo.Context;
using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> GetProducts()
        {
            return _context.Products;
        }

        public Product GetProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if(product is null)
            {
                throw new InvalidOperationException("Products is null");
            }

            return product;
        }

        public Product Create(Product product)
        {
            if (product is null)
            {
                throw new InvalidOperationException("Products is null");
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public bool Update(Product product)
        {
            if (product is null)
            {
                throw new InvalidOperationException("Products is null");
            }

            if (_context.Products.Any(p => p.ProductId == product.ProductId)) // se o productId for igual ao p q estou recebendo
            {
                _context.Products.Update(product);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product is not null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
