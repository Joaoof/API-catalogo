using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    //[ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository; // instância 

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        } // instância do context injetada no controlador

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _repository.GetProducts().ToList();

            if (products is null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("{id:int:min(1)}", Name="GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _repository.GetProduct(id);

            if (product is null)
            {
                return NotFound("Product Not Found");
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post([FromBody]Product product)
        {

            if (product is null)
            {
                return BadRequest();
            }

            var newProduct = _repository.Create(product);

            return new CreatedAtRouteResult("GetProduct", new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody]Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            bool att = _repository.Update(product);

            if (att)
            {
                return Ok(product);
            } else
            {
                return StatusCode(500, $"Failed to update product id = {id}");
            }
        }

        [HttpDelete]
        public ActionResult<Product> Delete(int id)
        {
            bool delete = _repository.Delete(id);

            if (delete)
            {
                return Ok($"Product id = {id} has been deleted");
            } else
            {
                return StatusCode(500, $"Failed to delete product id={id}");
            }
        }
    }
}
