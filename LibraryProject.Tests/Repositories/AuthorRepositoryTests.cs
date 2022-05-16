using LibraryProject.API.Repositories;
using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Repositories
{
    public class AuthorRepositoryTests
    {
        private readonly DbContextOptions<LibraryProjectContext> _options;
        private readonly LibraryProjectContext _context;
        private readonly AuthorRepository _authorRepository;

        public AuthorRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProjekt_InMemoryDatabase_Authors")
                .Options;

            _context = new(_options);
            _authorRepository = new(_context);
        }

        [Fact]
        public async void SelectAllAuthors_ShouldReturnListOfAuthors_WhenAuthorsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Author.Add(new()
            {
                Id = 1,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            });
            _context.Author.Add(new()
            {
                Id = 2,
                FirstName = "Helle",
                LastName = "Helle",
                MiddleName = "",
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _authorRepository.SelectAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Author>>(result); // check if (result) type is equal to "Author"
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllAuthors_ShouldReturnEmptyListOfAuthors_WhenNoAuthorsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _authorRepository.SelectAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Author>>(result); // check if (result) type is equal to "Author"
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectAuthorById_ShouldReturnAuthor_WhenAuthorExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int authorId = 1;

            _context.Author.Add(new()
            {
                Id = authorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _authorRepository.SelectAuthorById(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.Id);
        }

        [Fact]
        public async void SelectAuthorById_ShouldReturnNull_WhenAuthorDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _authorRepository.SelectAuthorById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewAuthor_ShouldFailToAddNewAuthor_WhenAuthorIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int authorId = 1;

            Author author = new()
            {
                Id = authorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            // Act
            async Task action() => await _authorRepository.InsertNewAuthor(author);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added.", ex.Message);
        }

        [Fact]
        public async void InsertNewAuthor_ShouldAddNewIdToAuthor_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Author author = new()
            {
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            // Act
            var result = await _authorRepository.InsertNewAuthor(author);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void UpdateExistingAuthor_ShouldChangeValuesOnAuthor_WhenAuthorExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int authorId = 1;

            Author newAuthor = new()
            {
                Id = authorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _context.Author.Add(newAuthor);
            await _context.SaveChangesAsync();

            Author updateAuthor = new()
            {
                Id = authorId,
                FirstName = "updated Astrid",
                LastName = "updated Lindgren",
                MiddleName = "",
            };

            // Act
            var result = await _authorRepository.UpdateExistingAuthor(authorId, updateAuthor);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.Id);
            Assert.Equal(updateAuthor.FirstName, result.FirstName);
            Assert.Equal(updateAuthor.LastName, result.LastName);
            Assert.Equal(updateAuthor.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void UpdateExistingAuthor_ShouldReturnNull_WhenAuthorDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int authorId = 1;

            Author updateAuthor = new()
            {
                Id = authorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            // Act
            var result = await _authorRepository.UpdateExistingAuthor(authorId, updateAuthor);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteAuthorById_ShouldReturnDeletedAuthor_WhenAuthorIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int authorId = 1;

            Author newAuthor = new()
            {
                Id = authorId,
                FirstName = "Astrid",
                LastName = "Lindgren",
                MiddleName = "",
            };

            _context.Author.Add(newAuthor);
            await _context.SaveChangesAsync();

            // Act
            var result = await _authorRepository.DeleteAuthor(authorId);
            var author = await _authorRepository.SelectAuthorById(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.Id);
            Assert.Null(author);
        }

        [Fact]
        public async void DeleteAuthorById_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _authorRepository.DeleteAuthor(1);

            // Assert
            Assert.Null(result);
        }
    }
}
