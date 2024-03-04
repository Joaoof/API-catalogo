using ApiCatalogo.Context;
using ApiCatalogo.DTOs.Products;
using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<ProductDTO>> Get([FromQuery] ProductsParams productsParams)
        {
            var products = _unitOfWork.ProductRepository.GetProducts(productsParams);

            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious,
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

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

        [HttpPatch("{id}/UpdatePartial")]
        public ActionResult<ProductDTOUpdateResponse> Patch(int id, [FromBody]JsonPatchDocument<ProductDTOUpdateRequest> patchProductDto)
        {
            if (patchProductDto is null || id <= 0)
            {
                return BadRequest();
            }

            var product = _unitOfWork.ProductRepository.Get(c => c.ProductId == id);

            if (product is null)
            {
                return NotFound();
            }

            var productUpdateRequest = _mapper.Map<ProductDTOUpdateRequest>(product);

            patchProductDto.ApplyTo(productUpdateRequest, ModelState);

            if (!ModelState.IsValid || TryValidateModel(productUpdateRequest))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(productUpdateRequest, product);

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();

            return Ok(_mapper.Map<ProductDTOUpdateResponse>(product));
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
