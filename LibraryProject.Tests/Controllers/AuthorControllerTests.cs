using LibraryProject.API.Services;
using LibraryProject.Controllers;
using LibraryProject.DTO_s;
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
    public class AuthorControllerTests
    {
        private readonly AuthorController _authorController;
        private readonly Mock<IAuthorService> _mockAuthorService = new();

        public AuthorControllerTests()
        {
            _authorController = new(_mockAuthorService.Object);
        }

        [Fact]
        public async void getAll_ShouldReturnStatusCode200_WhenAuthorsExists()
        {
            //  Arrange
            List<AuthorResponse> authors = new();

            authors.Add(new()
            {
                Id = 1,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            }); ;

            authors.Add(new()
            {
                Id = 2,
                FirstName = "Helle",
                LastName = "Helle",
                MiddleName = "",
            });

            _mockAuthorService
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(authors);

            //  Act
            var result = await _authorController.getAll();

            //  Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void getAll_ShouldReturnStatusCode204_WhenNoAuthorsExists()
        {
            //  Arrange
            List<AuthorResponse> authors = new();

            _mockAuthorService
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(authors);

            //  Act
            var result = await _authorController.getAll();

            //  Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void getAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //  Arrange
            _mockAuthorService
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(() => null);

            //  Act
            var result = await _authorController.getAll();

            //  Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void getAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //  Arrange
            _mockAuthorService
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            //  Act
            var result = await _authorController.getAll();

            //  Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange
            int authorId = 1;

            AuthorResponse author = new()
            {
                Id = authorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _mockAuthorService
                .Setup(x => x.GetAuthorById(It.IsAny<int>()))
                .ReturnsAsync(author);

            //Act
            var result = await _authorController.GetById(authorId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async Task GetById_ShouldReturnStatusCode404_WhenAuthorDoesNotExist()
        {
            //Arrange
            int authorId = 1;

            _mockAuthorService
                .Setup(x => x.GetAuthorById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _authorController.GetById(authorId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            _mockAuthorService
                .Setup(x => x.GetAuthorById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _authorController.GetById(1);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturnStatusCode200_WHenAuthorIsSuccessfullyCreated()
        {
            //Arrange
            AuthorRequest newAuthor = new()
            {
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            int authorId = 1;

            AuthorResponse authorResponse = new()
            {
                Id = authorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _mockAuthorService
                .Setup(x => x.CreateAuthor(It.IsAny<AuthorRequest>()))
                .ReturnsAsync(authorResponse);

            //Act
            var result = await _authorController.Create(newAuthor);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExcetionIsRaised()
        {
            //Arrange
            AuthorRequest newAuthor = new()
            {
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _mockAuthorService
                .Setup(x => x.CreateAuthor(It.IsAny<AuthorRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _authorController.Create(newAuthor);


            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenAUthorIsSuccessfullyUpdated()
        {
            //Arrange
            AuthorRequest updateAuthor = new()
            {
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            int authorId = 1;

            AuthorResponse authorResponse = new()
            {
                Id = authorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _mockAuthorService
                .Setup(x => x.UpdateAuthor(It.IsAny<int>(), It.IsAny<AuthorRequest>()))
                .ReturnsAsync(authorResponse);

            //Act
            var result = await _authorController.Update(authorId, updateAuthor);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenTryingToUpdateAuthorWhichDoesNotExist()
        {
            //Arrange
            AuthorRequest updateAuthor = new()
            {
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            int authorId = 1;

            _mockAuthorService
                .Setup(x => x.UpdateAuthor(It.IsAny<int>(), It.IsAny<AuthorRequest>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _authorController.Update(authorId, updateAuthor);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            AuthorRequest updateAuthor = new()
            {
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            int authorId = 1;

            _mockAuthorService
                .Setup(x => x.UpdateAuthor(It.IsAny<int>(), It.IsAny<AuthorRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _authorController.Update(authorId, updateAuthor);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenAuthorIsDeleted()
        {
            //Arrange
            int authorId = 1;

            AuthorResponse authorResponse = new()
            {
                Id = authorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _mockAuthorService
                .Setup(x => x.DeleteAuthor(It.IsAny<int>()))
                .ReturnsAsync(authorResponse);

            //Act
            var result = await _authorController.Delete(authorId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTryingToDeleteAuthorWhichDoesNotExist()
        {
            //Arrange
            int authorId = 1;

            _mockAuthorService
                .Setup(x => x.DeleteAuthor(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _authorController.Delete(authorId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int authorId = 1;

            _mockAuthorService
                .Setup(x => x.DeleteAuthor(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _authorController.Delete(authorId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
