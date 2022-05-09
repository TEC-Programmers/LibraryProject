using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using LibraryProject.Database.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategories();


    }
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            List<Category> categories = await _categoryRepository.SelectAllCategories();

            if (categories != null)
            {
                return categories.Select(category => MapCategoryToCategoryResponse(category)).ToList();
            }

            return null;
        }
        private CategoryResponse MapCategoryToCategoryResponse(Category categories)
        {
            return new CategoryResponse
            {
                Id = categories.Id,
                CategoryName = categories.CategoryName,
            };
        }
    }
}
