using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;


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

            var categoriesDto = categories.ToCategoryDTOList();

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

            var categoryDto = category.ToCategoryDTO();

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

            var category = categoryDto.ToCategory();

            var createCategory = _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Commit();

            var newCategoryDto = createCategory.ToCategoryDTO();

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

            var category = categoryDto.ToCategory();


            var categoryUpdate = _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();

            var categoryUpdatedDto = category.ToCategoryDTO();

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

            var categoryExcludedDto = category.ToCategoryDTO();

            return Ok(categoryExcludedDto);
        }
    }


}
