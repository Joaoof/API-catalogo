using ApiCatalogo.Context;
using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;
using ApiCatalogo.PagedList;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        public PagedList<Product> GetProducts(ProductsParams productsParams)
        {
            var products = GetAll().OrderBy(p => p.ProductId).AsQueryable();
            var productsOrder = PagedList<Product>.ToPagedList(products, productsParams.PageNumber, productsParams.PageSize );

            return productsOrder;
        } // A fórmula para calcular o índice inicial é: (número da página - 1) * tamanho da página. Por exemplo, se estamos na página 2 com 5 produtos por página, o índice inicial será: (2 - 1) * 5 = 5. Isso significa que vamos pular os primeiros 5 produtos.

        public IEnumerable<Product> GetProductsCategories(int id)
        {
            return GetAll().Where(c => c.CategoryId == id);
        }
    }
}
