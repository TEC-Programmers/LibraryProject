using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> SelectAllCategories();
        Task<Category> SelectCategoryById(int categoryId);
        Task<List<Category>> SelectAllCategoriesWithoutBooks();
        Task<Category> InsertNewCategory(Category category);
        Task<Category> UpdateExistingCategory(int categoryId, Category category);
        Task<Category> DeleteCategoryById(int categoryId);
    }
    public class CategoryRepository:ICategoryRepository
    {
        private readonly LibraryProjectContext _context;

        public CategoryRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> SelectAllCategories()
        {
            return await _context.Category
                .Include(b => b.Books)
                .ToListAsync();
        }
        public async Task<Category> SelectCategoryById(int categoryId)
        {
            return await _context.Category
                .Include(a => a.Books)
                .FirstOrDefaultAsync(category => category.Id == categoryId);
        }
        public async Task<List<Category>> SelectAllCategoriesWithoutBooks()
        {
            return await _context.Category
                        .ToListAsync();
        }
        public async Task<Category> InsertNewCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<Category> UpdateExistingCategory(int categoryId, Category category)
        {
            Category updateCategory = await _context.Category.FirstOrDefaultAsync(category => category.Id == categoryId);
            if (updateCategory != null)
            {
                updateCategory.CategoryName = category.CategoryName;

                await _context.SaveChangesAsync();
            }
            return updateCategory;
        }
        public async Task<Category> DeleteCategoryById(int categoryId)
        {
            Category deleteCategory = await _context.Category.FirstOrDefaultAsync(category => category.Id == categoryId);
            if (deleteCategory != null)
            {

                _context.Remove(deleteCategory);
                await _context.SaveChangesAsync();
            }
            return deleteCategory;
        }
    }
}
