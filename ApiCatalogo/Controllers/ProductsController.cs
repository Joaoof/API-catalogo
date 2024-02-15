using ApiCatalogo.Context;
using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    //[ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("products/{id}")]
        public ActionResult <IEnumerable<Product>> GetProductsCategory(int id)
        {
            var product = _unitOfWork.ProductRepository.GetProductsCategories(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _unitOfWork.ProductRepository.GetAll();

            if (products is null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("{id:int:min(1)}", Name="GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(c => c.ProductId == id);

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

            var newProduct = _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("GetProduct", new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody]Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var productAtt = _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();

            return Ok(productAtt);
        }

        [HttpDelete]
        public ActionResult<Product> Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(c => c.ProductId == id);
              
            if (product is null)
            {
                return NotFound("Product not found......");
            }

            var productDelete = _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Commit();
            
            return Ok(productDelete);

        }
    }
}
