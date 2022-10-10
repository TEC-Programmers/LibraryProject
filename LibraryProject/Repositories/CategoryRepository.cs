using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    //creating Interface of ICategoryRepository
    public interface ICategoryRepository
    {
        Task<List<Category>> SelectAllCategoriesWithBooks();
        Task<Category> SelectCategoryById(int categoryId);
        Task<List<Category>> SelectAllCategoriesWithProcedure();
        Task<Category> InsertNewCategoryWithProcedure(Category category);
        Task<Category> UpdateExistingCategory(int categoryId, Category category);
        Task<Category> DeleteCategoryByIdWithProcedure(int categoryId);
    }
    public class CategoryRepository: ICategoryRepository      // This class is inheriting interfcae ICategoryRepository and implement the interfaces
    {
        private readonly LibraryProjectContext _context;  //making an instance of the class LibraryProjectContext
        public string _connectionString;

        public CategoryRepository(LibraryProjectContext context, IConfiguration configuration)    //dependency injection with parameter 
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("Default");
        }
        //implementing the methods of ICategoryRepository interface 
        public async Task<Category> InsertNewCategoryWithProcedure(Category category)
        {
            using SqlConnection sql = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("insertCategory", sql);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CategoryName", category.CategoryName));

            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return category;
        }
        public async Task<List<Category>> SelectAllCategoriesWithProcedure()
        {
            return await _context.Category.FromSqlRaw("selectAllCategories").ToListAsync();
        }
        public async Task<Category> DeleteCategoryByIdWithProcedure(int categoryId)
        {
            Category deleteCategory = await _context.Category
                .FirstOrDefaultAsync(u => u.Id == categoryId);

            var parameter = new List<SqlParameter>
            {
               new SqlParameter("@Id", categoryId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC deleteCategory @Id", parameter.ToArray());
            return deleteCategory;
        }
        public async Task<List<Category>> SelectAllCategoriesWithBooks()
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
    }
}
