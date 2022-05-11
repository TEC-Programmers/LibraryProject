﻿using LibraryProject.API.DTO_s;
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
        Task<CategoryResponse> GetCategoryById(int categoryId);
        Task<List<CategoryResponse>> GetAllCategoriesWithoutBooks();
        //Task<CategoryResponse> CreateCategory(CategoryRequest newCategory);
        //Task<CategoryResponse> UpdateCategory(int categoryId, CategoryRequest updateCategory);
        //Task<CategoryResponse> DeleteCategory(int categoryId);


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

        public async Task<CategoryResponse> GetCategoryById(int categoryId)
        {
            Category category = await _categoryRepository.SelectCategoryById(categoryId);

            if (category != null)
            {

                return MapCategoryToCategoryResponse(category);
            }
            return null;
        }
        public async Task<List<CategoryResponse>> GetAllCategoriesWithoutBooks()
        {
            List<Category> categories = await _categoryRepository.SelectAllCategoriesWithoutBooks();

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
                Books = categories.Books.Select(category => new CategoryBookResponse
                {
                    Id = category.Id,
                    Title = category.Title,
                    Description = category.Description,
                    Language = category.Language,
                    PublishYear = category.PublishYear
                }).ToList()
            };
        }
    }
}
