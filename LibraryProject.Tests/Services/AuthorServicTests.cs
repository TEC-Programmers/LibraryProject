using LibraryProject.API.Repositories;
using LibraryProject.API.Services;
using LibraryProject.Database.Entities;
using LibraryProject.DTO_s;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Services
{
    public class AuthorServicTests
    {
        private readonly AuthorService _AuthorService;
        private readonly Mock<IAuthorRepository> _mockAuthorRepository = new();
        private readonly Mock<IBookRepository> _mockBookRepository = new();
        public AuthorServicTests()
        {
            _AuthorService = new AuthorService(_mockAuthorRepository.Object, _mockBookRepository.Object);
        }

        [Fact]
        public async void GetAllAuthors_ShouldReturnListOfAuthorResponses_WhenAuthorsExists()
        {
            // Arrange 
            List<Author> Authors = new();

            Authors.Add(new()
            {
                Id = 1,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            });

            Authors.Add(new()
            {
                Id = 2,
                FirstName = "Helle",
                LastName = "Helle",
                MiddleName = "",
            });

            _mockAuthorRepository
                .Setup(x => x.SelectAllAuthors())
                .ReturnsAsync(Authors);

            // Act
            var result = await _AuthorService.GetAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<AuthorResponse>>(result);
        }

        [Fact]
        public async void GetAllAuthors_ShouldReturnEmptyListOfAuthorResponses_WhenNoAuthorsExists()
        {
            // Arrange 
            List<Author> Authors = new();

            _mockAuthorRepository
                .Setup(x => x.SelectAllAuthors())
                .ReturnsAsync(Authors);

            // Act
            var result = await _AuthorService.GetAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<AuthorResponse>>(result);
        }

        [Fact]
        public async void GetAuthorsById_ShouldReturnAuthorResponse_WhenAuthorExists()
        {
            // Arrange 
            int AuthorId = 1;

            Author Author = new()
            {
                Id = AuthorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _mockAuthorRepository
                .Setup(x => x.SelectAuthorById(It.IsAny<int>()))
                .ReturnsAsync(Author);

            // Act
            var result = await _AuthorService.GetAuthorById(AuthorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(AuthorId, result.Id);
            Assert.Equal(Author.FirstName, result.FirstName);
            Assert.Equal(Author.LastName, result.LastName);
            Assert.Equal(Author.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void GetAuthorsById_ShouldReturnNull_WhenAuthorDoesNotExists()
        {
            // Arrange 
            int AuthorId = 1;

            _mockAuthorRepository
                .Setup(x => x.SelectAuthorById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _AuthorService.GetAuthorById(AuthorId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateAuthor_ShouldReturnAuthorResponse_WhenCreateIsSuccess()
        {
            // Arrange 
            AuthorRequest newAuthor = new()
            {
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            int AuthorId = 1;

            Author createdAuthor = new()
            {
                Id = AuthorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _mockAuthorRepository
                .Setup(x => x.InsertNewAuthor(It.IsAny<Author>()))
                .ReturnsAsync(createdAuthor);

            // Act
            var result = await _AuthorService.CreateAuthor(newAuthor);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(AuthorId, result.Id);
            Assert.Equal(newAuthor.FirstName, result.FirstName);
            Assert.Equal(newAuthor.LastName, result.LastName);
            Assert.Equal(newAuthor.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void CreateAuthor_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            // Arrange 
            AuthorRequest newAuthor = new()
            {
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _mockAuthorRepository
                .Setup(x => x.InsertNewAuthor(It.IsAny<Author>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _AuthorService.CreateAuthor(newAuthor);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateAuthor_ShouldReturnAuthorResponse_WhenUpdateIsSuccess()
        {
            // Arrange 
            AuthorRequest AuthorRequest = new()
            {
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            int AuthorId = 1;

            Author Author = new()
            {
                Id = AuthorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _mockAuthorRepository
                .Setup(x => x.UpdateExistingAuthor(It.IsAny<int>(), It.IsAny<Author>()))
                .ReturnsAsync(Author);

            // Act
            var result = await _AuthorService.UpdateAuthor(AuthorId, AuthorRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(AuthorId, result.Id);
            Assert.Equal(AuthorRequest.FirstName, result.FirstName);
            Assert.Equal(AuthorRequest.LastName, result.LastName);
            Assert.Equal(AuthorRequest.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void UpdateAuthor_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange 
            AuthorRequest AuthorRequest = new()
            {
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            int AuthorId = 1;

            _mockAuthorRepository
                .Setup(x => x.UpdateExistingAuthor(It.IsAny<int>(), It.IsAny<Author>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _AuthorService.UpdateAuthor(AuthorId, AuthorRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteAuthor_ShouldReturnAuthorResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int AuthorId = 1;

            Author deletedAuthor = new()
            {
                Id = 1,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _mockAuthorRepository
                .Setup(x => x.DeleteAuthor(It.IsAny<int>()))
                .ReturnsAsync(deletedAuthor);

            // Act
            var result = await _AuthorService.DeleteAuthor(AuthorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(AuthorId, result.Id);
        }

        [Fact]
        public async void DeleteAuthor_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            int AuthorId = 1;

            _mockAuthorRepository
                .Setup(x => x.DeleteAuthor(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _AuthorService.DeleteAuthor(AuthorId);

            // Assert
            Assert.Null(result);
        }
    }
}
