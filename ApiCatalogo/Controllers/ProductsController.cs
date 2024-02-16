using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("products/{id}")]
        public ActionResult<IEnumerable<ProductDTO>> GetProductsCategory(int id)
        {
            var products = _unitOfWork.ProductRepository.GetProductsCategories(id);

            if (products is null)
            {
                return NotFound();
            }

            // var destino = _mapper.Map<Destino>(origem);
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            var products = _unitOfWork.ProductRepository.GetAll();

            if (products is null)
            {
                return NotFound();
            }

            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDto);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
        public ActionResult<ProductDTO> Get(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(c => c.ProductId == id);

            if (product is null)
            {
                return NotFound("Product Not Found");
            }

            var productsDto = _mapper.Map<ProductDTO>(product);

            return Ok(productsDto);
        }

        [HttpPost]
        public ActionResult<ProductDTO> Post([FromBody] ProductDTO productDto)
        {

            if (productDto is null)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDto);

            var newProduct = _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.Commit();

            var newProductDto = _mapper.Map<ProductDTO>(newProduct);

            return new CreatedAtRouteResult("GetProduct", new { id = newProductDto.ProductId }, newProductDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProductDTO> Put(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDto);

            var productAtt = _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();

            var productAttDto = _mapper.Map<ProductDTO>(productAtt);

            return Ok(productAttDto);
        }

        [HttpDelete]
        public ActionResult<ProductDTO> Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(c => c.ProductId == id);

            if (product is null)
            {
                return NotFound("Product not found......");
            }

            var productDelete = _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Commit();

            var productDeleteDto = _mapper.Map<ProductDTO>(productDelete);

            return Ok(productDeleteDto);

        }
    }
}
