using LibraryProject.API.Controllers;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Controllers
{
    public class CategoryControllerTests
    {
        private readonly CategoryController _categoryController;
        private readonly Mock<ICategoryService> _mockCategoryService = new();

        public CategoryControllerTests()
        {
            _categoryController = new(_mockCategoryService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenCategoriesExist()
        {
            //Arrange

            List<CategoryResponse> categories = new();
            categories.Add(new()
            {
                Id = 1,
                CategoryName = "BørneBog"

            });
            categories.Add(new()
            {
                Id = 2,
                CategoryName = "Roman"

            });

            _mockCategoryService.Setup(x => x.GetAllCategories()).ReturnsAsync(categories);



            //Act
            var result = await _categoryController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoCategoriesExists()
        {
            //Arrange
            List<CategoryResponse> products = new();


            _mockCategoryService
                .Setup(x => x.GetAllCategories())
                .ReturnsAsync(products);



            //Act
            var result = await _categoryController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //Arrange



            _mockCategoryService
                .Setup(x => x.GetAllCategories())
                .ReturnsAsync(() => null);



            //Act
            var result = await _categoryController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange



            _mockCategoryService
                .Setup(x => x.GetAllCategories())
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _categoryController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange
            int categoryId = 1;

            CategoryResponse category = new()

            {
                Id = 1,
                CategoryName = "Toy"

            };


            _mockCategoryService
                .Setup(x => x.GetCategoryById(It.IsAny<int>()))
                .ReturnsAsync(category);



            //Act
            var result = await _categoryController.GetById(categoryId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenCategoryDoesNotExist()
        {
            //Arrange
            int categoryId = 1;



            _mockCategoryService
                .Setup(x => x.GetCategoryById(It.IsAny<int>()))
                .ReturnsAsync(() => null);



            //Act
            var result = await _categoryController.GetById(categoryId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockCategoryService
                .Setup(x => x.GetCategoryById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));



            //Act
            var result = await _categoryController.GetById(1);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenCategoryIsSuccessfullyCreated()
        {
            //Arrange


            CategoryRequest newCategory = new()

            {


                CategoryName = "BørneBog"


            };

            //int productId = 1;

            CategoryResponse categoryResponse = new()
            {
                Id = 1,
                CategoryName = "BørneBog"

            };
            _mockCategoryService
                .Setup(x => x.CreateCategory(It.IsAny<CategoryRequest>()))
                .ReturnsAsync(categoryResponse);



            //Act
            var result = await _categoryController.Create(newCategory);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange


            CategoryRequest newCategory = new()

            {

                CategoryName = "BørneBog"

            };


            _mockCategoryService
                .Setup(x => x.CreateCategory(It.IsAny<CategoryRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _categoryController.Create(newCategory);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenCategoryIsSuccessfullyUpdated()
        {
            //Arrange


            CategoryRequest updateCategory = new()

            {
                CategoryName = "BørneBog"

            };

            int categoryId = 1;


            CategoryResponse categoryResponse = new()
            {
                Id = 1,
                CategoryName = "BørneBog"

            };
            _mockCategoryService
                .Setup(x => x.UpdateCategory(It.IsAny<int>(), It.IsAny<CategoryRequest>()))
                .ReturnsAsync(categoryResponse);



            //Act
            var result = await _categoryController.Update(categoryId, updateCategory);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenTryingToUpdateCategoryWhichDoesNotExist()
        {
            //Arrange


            CategoryRequest updateCategory = new()

            {
                CategoryName = "BørneBog"

            };



            _mockCategoryService
                .Setup(x => x.UpdateCategory(It.IsAny<int>(), It.IsAny<CategoryRequest>()))
                .ReturnsAsync(() => null);



            //Act
            int categoryId = 1;

            var result = await _categoryController.Update(categoryId, updateCategory);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange


            CategoryRequest updateCategory = new()

            {
                CategoryName = "BørneBog"

            };

            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.UpdateCategory(It.IsAny<int>(), It.IsAny<CategoryRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _categoryController.Update(categoryId, updateCategory);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenCategoryIsDeleted()
        {
            //Arrange
            int categoryId = 1;

            CategoryResponse categoryResponse = new()
            {
                Id = 1,
                CategoryName = "BørneBog"

            };
            _mockCategoryService
                .Setup(x => x.DeleteCategory(It.IsAny<int>()))
                .ReturnsAsync(categoryResponse);



            //Act
            var result = await _categoryController.Delete(categoryId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTryingToDeleteProductWhichDoesNotExist()
        {
            //Arrange



            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.DeleteCategory(It.IsAny<int>()))
                .ReturnsAsync(() => null);



            //Act
            var result = await _categoryController.Delete(categoryId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange


            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.DeleteCategory(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _categoryController.Delete(categoryId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
    }
}
