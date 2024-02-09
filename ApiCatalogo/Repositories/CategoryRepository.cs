using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategoriesProd(int CategoryId)
        {
            return _context.Categories.Include(p => p.Products).Where(c => c.CategoryId <= 5).ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
           return _context.Categories.ToList(); 
        }
        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public Category Create(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;

        }
        public Category Update(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return category;
        }

        public Category Delete(int id)
        {
            var category = _context.Categories.Find(id);

            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return category; 
        }

    }
}
