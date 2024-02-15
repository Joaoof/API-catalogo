using ApiCatalogo.DTOs;
using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;


namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IUnitOfWork unitOfWork, ILogger<CategoriesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {

            var categories = _unitOfWork.CategoryRepository.GetAll();

            var categoriesDto = new List<CategoryDTO>();

            foreach (var category in categories)
            {
                var categoryDTO = new CategoryDTO()
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    ImageUrl = category.ImageUrl,
                };

                categoriesDto.Add(categoryDTO);
            }

                return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<CategoryDTO> GetById(int id)
        {

            var category = _unitOfWork.CategoryRepository.Get(c => c.CategoryId == id);

            if (category is null)
            {
                _logger.LogWarning($"Category with id = {id} not found");
                return NotFound($"Category com o id= {id} not found");
            }

            var categoryDto = new CategoryDTO()
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            };

            return Ok(categoryDto);
        }

        [HttpPost]
        public ActionResult<CategoryDTO> Post(CategoryDTO categoryDto)
        {
            if (categoryDto is null)
            {
                _logger.LogWarning($"Invalid Data");
                return BadRequest("Invalid Data");
            }

            var category = new Category()
            {
                CategoryId = categoryDto.CategoryId,
                Name = categoryDto.Name,
                ImageUrl = categoryDto.ImageUrl
            }; // converto dto pra categoria

            var createCategory = _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Commit();

            var newCategoryDto = new CategoryDTO()
            {
                CategoryId = createCategory.CategoryId,
                Name = createCategory.Name,
                ImageUrl = createCategory.ImageUrl
            }; // reverto de categoria pra dto

            return new CreatedAtRouteResult("GetCategory", new { id = newCategoryDto.CategoryId }, newCategoryDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoryDTO> Put(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                _logger.LogWarning($"Invalid Data");
                return BadRequest("Invalid Data");
            }

            var category = new Category()
            {
                CategoryId = categoryDto.CategoryId,
                Name = categoryDto.Name,
                ImageUrl = categoryDto.ImageUrl
            }; // converto dto pra categoria


            var categoryUpdate = _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();

            var categoryUpdatedDto = new CategoryDTO()
            {
                CategoryId = categoryUpdate.CategoryId,
                Name = categoryUpdate.Name,
                ImageUrl = categoryUpdate.ImageUrl
            }; // reverto de categoria pra dto

            return Ok(categoryUpdatedDto);
        }

        [HttpDelete]
        public ActionResult<CategoryDTO> Delete(int id)
        {
            var category = _unitOfWork.CategoryRepository.Get(c => c.CategoryId == id);

            if (category is null)
            {
                _logger.LogWarning($"Category with id= {id} not found");
                return NotFound("Category not found");
            }

            var categoryDeleted = _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Commit();

            var categoryExcludedDto = new CategoryDTO()
            {
                CategoryId = categoryDeleted.CategoryId,
                Name = categoryDeleted.Name,
                ImageUrl = categoryDeleted.ImageUrl
            };

            return Ok(categoryExcludedDto);
        }
    }


}
