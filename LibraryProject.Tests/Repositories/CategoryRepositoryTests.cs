using LibraryProject.API.Repositories;
using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Repositories
{
    public class CategoryRepositoryTests
    {
        private readonly DbContextOptions<LibraryProjectContext> _options;
        private readonly LibraryProjectContext _context;
        private readonly CategoryRepository _categoryRepository;
        private readonly IConfiguration _configuration;
        public CategoryRepositoryTests(IConfiguration configuration)
        {
            _configuration = configuration;
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProjectContext")
                .Options;

            _context = new(_options);
            _categoryRepository = new(_context, _configuration);
        }
        [Fact]
        public async void SelectAllCategories_ShouldReturnListOfCategories_WhenCategoryExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            _context.Category.Add(new()
            {
                Id = 1,
                CategoryName = "BørneBog"

            });
            _context.Category.Add(new()
            {

                Id = 2,
                CategoryName = "Roman"



            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _categoryRepository.SelectAllCategoriesWithProcedure();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Equal(2, result.Count);
            // Assert.Empty(result);
        }
        [Fact]
        public async void SelectAllCategories_ShouldReturnEmptyListOfCategories_WhenCategoryExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();




            //Act
            var result = await _categoryRepository.SelectAllCategoriesWithProcedure();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);

            Assert.Empty(result);
        }
        [Fact]
        public async void SelectCategoryById_ShouldReturnCategory_WhenCategoryExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            _context.Category.Add(new()
            {
                Id = 1,
                CategoryName = "Toy"


            });


            await _context.SaveChangesAsync();

            //Act
            var result = await _categoryRepository.SelectCategoryById(categoryId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);
            // Assert.Empty(result);
        }
        [Fact]
        public async void SelectCategoryById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();




            //Act
            var result = await _categoryRepository.SelectCategoryById(1);

            //Assert


            Assert.Null(result);
        }
        [Fact]
        public async void InsertNewCategory_ShouldAddnewIdToCategory_WhenSavingToDatabase()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Category category = new()
            {
                Id = 1,
                CategoryName = "Toy"


            };


            await _context.SaveChangesAsync();

            //Act
            var result = await _categoryRepository.InsertNewCategoryWithProcedure(category);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(expectedNewId, result.Id);

        }

        [Fact]
        public async void InsertNewCategory_ShouldFailToAddNewCategory_WhenCategoryIdAlreadyExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();



            Category category = new()
            {
                Id = 1,
                CategoryName = "Toy"

            };

            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            //Act
            async Task action() => await _categoryRepository.InsertNewCategoryWithProcedure(category);


            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);

        }
        [Fact]
        public async void UpdateExistingCategory_ShouldChangeValuesOnCategory_WhenCategoryExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            Category newCategory = new()
            {
                Id = categoryId,
                CategoryName = "Toy"

            };

            _context.Category.Add(newCategory);
            await _context.SaveChangesAsync();

            Category updateCategory = new()
            {
                Id = categoryId,
                CategoryName = "updated Toy"

            };



            //Act
            var result = await _categoryRepository.UpdateExistingCategory(categoryId, updateCategory);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(updateCategory.CategoryName, result.CategoryName);

        }

        [Fact]
        public async void UpdateExistingCategory_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;


            Category updateCategory = new()
            {
                Id = categoryId,
                CategoryName = "Toy"

            };



            //Act
            var result = await _categoryRepository.UpdateExistingCategory(categoryId, updateCategory);

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public async void DeleteCategoryById_ShouldReturnDeleteCategory_WhenCategoryIsDeleted()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            Category newCategory = new()
            {
                Id = categoryId,
                CategoryName = "Toy"

            };

            _context.Category.Add(newCategory);
            await _context.SaveChangesAsync();


            //Act
            var result = await _categoryRepository.DeleteCategoryByIdWithProcedure(categoryId);
            var category = await _categoryRepository.SelectCategoryById(categoryId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Null(category);
        }
        [Fact]
        public async void DeleteCategoryById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();



            _context.Add(new Category { Id = 1, CategoryName = "Toy" });
            //Act
            var result = await _categoryRepository.DeleteCategoryByIdWithProcedure(1);


            //Assert

            Assert.Null(result);
        }
    }
}
