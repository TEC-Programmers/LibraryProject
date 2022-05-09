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
    }
}
