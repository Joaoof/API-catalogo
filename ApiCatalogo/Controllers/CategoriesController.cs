using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public CategoriesController(AppDbContext context, ILogger<CategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("UsedFromServices/{name}")]
        public ActionResult<string> GetSalutationFromServices([FromServices] IMyService myService, string name)
        {
            return myService.Salutation(name);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesProducts()
        {
            try
            {

                _logger.LogInformation(" ==================================GET api/categories/products"); 
                return await _context.Categories.Include(p => p.Products).Where(c => c.CategoryId <= 5).ToListAsync(); //carregar os relacionamento
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem with the request");
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {

            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            if (categories is null)
            {
                return NotFound("Categories not found");
            }

            return categories;
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> GetById(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
                if (category is null)
                {
                    _logger.LogWarning("====================================================================");
                    _logger.LogWarning($"Category with id = {id} not found");
                    _logger.LogWarning("====================================================================");
                    return NotFound($"Category com o id= {id} not found");
                }

                return category;
            }
            catch (Exception)
            {
                _logger.LogError("====================================================================");
                 _logger.LogError($"{StatusCodes.Status500InternalServerError}, There was a problem with the request");
                _logger.LogWarning("====================================================================");

                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem with the request");

            }
        }

        [HttpPost]
        public ActionResult<Category> Post(Category category)
        {
            if (category is null)
            {
                return BadRequest("Invalid Data");
            }
            _context.Categories.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Category> Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Invalid Data");
            }

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete]
        public ActionResult<Category> Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

            Console.WriteLine(category);

            if (category is null)
            {
                return NotFound("Category not found");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok(category);
        }
    }
}
