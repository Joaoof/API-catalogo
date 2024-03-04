using ApiCatalogo.Context;
using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        public IEnumerable<Product> GetProducts(ProductsParams productsParams)
        {
            return GetAll().OrderBy(p => p.Name).Skip((productsParams.PageNumber - 1) * productsParams.PageSize).Take(productsParams.PageSize)
                .ToList();
        } // A fórmula para calcular o índice inicial é: (número da página - 1) * tamanho da página. Por exemplo, se estamos na página 2 com 5 produtos por página, o índice inicial será: (2 - 1) * 5 = 5. Isso significa que vamos pular os primeiros 5 produtos.

        public IEnumerable<Product> GetProductsCategories(int id)
        {
            return GetAll().Where(c => c.CategoryId == id);
        }
    }
}
