using ApiCatalogo.DTOs.Categories;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, ILogger<CategoriesController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {

            var categories = _unitOfWork.CategoryRepository.GetAll();

            if (categories is null)
            {
                return NotFound();
            }

            var categoriesDto = _mapper.Map<CategoryDTO>(categories);

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

            var categoryDto = _mapper.Map<CategoryDTO>(category);

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

            var category = _mapper.Map<Category>(categoryDto);

            var createCategory = _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Commit();

            var newCategoryDto = _mapper.Map<CategoryDTO>(createCategory);

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

            var category = _mapper.Map<Category>(categoryDto);


            var categoryUpdate = _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();

            var categoryUpdatedDto = _mapper.Map<CategoryDTO>(categoryUpdate);

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

            var categoryExcludedDto = _mapper.Map<CategoryDTO>(categoryDeleted);

            return Ok(categoryExcludedDto);
        }
    }


}
