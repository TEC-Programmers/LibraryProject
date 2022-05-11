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

    }
}
