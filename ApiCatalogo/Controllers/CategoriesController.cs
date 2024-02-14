using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger _logger;

        public CategoriesController(ICategoryRepository repository, ILogger<CategoriesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts(int CategoryId)
        {

            var categoriesProducts = _repository.GetCategoriesProd(5);
            return Ok(categoriesProducts);
        } 
         

        [HttpGet]
        public  ActionResult<IEnumerable<Category>> Get()
        {

           var categories = _repository.GetCategories();

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> GetById(int id)
        {

                var category = _repository.GetCategory(id);  
       
                if (category is null)
                {
                    _logger.LogWarning($"Category with id = {id} not found");
                    return NotFound($"Category com o id= {id} not found");
                }

                return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> Post(Category category)
        {
            if (category is null)
            {
                _logger.LogWarning($"Invalid Data");
                return BadRequest("Invalid Data");
            }

            var createCategory= _repository.Create(category);

            return new CreatedAtRouteResult("GetCategory", new { id = createCategory.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Category> Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                _logger.LogWarning($"Invalid Data");
                return BadRequest("Invalid Data");
            }

            var categoryUpdate = _repository.Update(category);

            return Ok(categoryUpdate);
        }

        [HttpDelete]
        public ActionResult<Category> Delete(int id)
        {
            var category = _repository.GetCategory(id);

            if (category is null)
            {
                _logger.LogWarning($"Category with id= {id} not found");
                return NotFound("Category not found");
            }

            var categoryDeleted = _repository.Delete(id);
            return Ok(categoryDeleted);
        }
    }
}
