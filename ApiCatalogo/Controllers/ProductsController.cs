using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context; // instância 

        public ProductsController(AppDbContext context)
        {
            _context = context;
        } // instância do context injetada no controlador

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _context.Products.Take(10).ToListAsync(); // retorna uma lista de produtos
            if (products is null)
            {
                return NotFound("Products not found");
            }
            return products;
        }

        [HttpGet("{id:int:min(1)}", Name="GetProduct")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id); // localizando o primeiro elemento encontrado
            if (product is null)
            {
                return NotFound("Product not found");
            }
            return product;
        }

        [HttpPost]
        public ActionResult<Product> Post(Product product)
        {
            if(product is null)
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProduct", new { id = product.ProductId }, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Product> Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // o Entity vai identificar que essa entidade vai ser modificada
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete]
        public ActionResult<Product> Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p=> p.ProductId == id);

            if (product is null)
            {
                return NotFound("Product not found");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}
