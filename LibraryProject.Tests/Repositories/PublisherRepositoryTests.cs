using LibraryProject.API.Database.Entities;
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
    public class PublisherRepositoryTests
    {
        private readonly DbContextOptions<LibraryProjectContext> _options;
        private readonly LibraryProjectContext _context;
        private readonly PublisherRepository _publisherRepository;
        private readonly IConfiguration _configuration;

        public PublisherRepositoryTests(IConfiguration configuration)
        {
            _configuration = configuration;
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProjekt_InMemoryDatabase_Publishers")
                .Options;

            _context = new(_options);
            _publisherRepository = new(_context, _configuration);
        }

        [Fact]
        public async void SelectAllPublishers_ShouldReturnListOfPublishers_WhenPublishersExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Publisher.Add(new()
            {
                Id = 1,
                Name = "Gyldendal"
            });
            _context.Publisher.Add(new()
            {
                Id = 2,
                Name = "Rosinante"
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _publisherRepository.SelectAllPublishers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Publisher>>(result); // check if (result) type is equal to "Publisher"
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllPublishers_ShouldReturnEmptyListOfPublishers_WhenNoPublishersExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _publisherRepository.SelectAllPublishers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Publisher>>(result); // check if (result) type is equal to "Publisher"
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectPublisherById_ShouldReturnPublisher_WhenPublisherExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int publisherId = 1;

            _context.Publisher.Add(new()
            {
                Id = publisherId,
                Name = "Gyldendal"
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _publisherRepository.SelectPublisherById(publisherId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Publisher>(result);
            Assert.Equal(publisherId, result.Id);
        }

        [Fact]
        public async void SelectPublisherById_ShouldReturnNull_WhenPublisherDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _publisherRepository.SelectPublisherById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewPublisher_ShouldFailToAddNewPublisher_WhenpublisherIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int publisherId = 1;

            Publisher publisher = new()
            {
                Id = publisherId,
                Name = "Gyldendal"
            };

            _context.Publisher.Add(publisher);
            await _context.SaveChangesAsync();

            // Act
            async Task action() => await _publisherRepository.InsertNewPublisherWithProcedure(publisher);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added.", ex.Message);
        }

        [Fact]
        public async void InsertNewPublisher_ShouldAddNewIdToPublisher_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Publisher publisher = new()
            {
                Name = "Gyldendal"
            };

            // Act
            var result = await _publisherRepository.InsertNewPublisherWithProcedure(publisher);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Publisher>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void UpdateExistingPublisher_ShouldChangeValuesOnPublisher_WhenPublisherExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int publisherId = 1;

            Publisher newPublisher = new()
            {
                Id = publisherId,
                Name = "Gyldendal"
            };

            _context.Publisher.Add(newPublisher);
            await _context.SaveChangesAsync();

            Publisher updatePublisher = new()
            {
                Id = publisherId,
                Name = "UPDATED - Gyldendal"
            };

            // Act
            var result = await _publisherRepository.UpdateExistingPublisher(publisherId, updatePublisher);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Publisher>(result);
            Assert.Equal(publisherId, result.Id);
            Assert.Equal(updatePublisher.Name, result.Name);
        }

        [Fact]
        public async void UpdateExistingPublisher_ShouldReturnNull_WhenPublisherDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int publisherId = 1;

            Publisher updatePublisher = new()
            {
                Id = publisherId,
                Name = "Gyldendal"
            };

            // Act
            var result = await _publisherRepository.UpdateExistingPublisher(publisherId, updatePublisher);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeletePublisherById_ShouldReturnDeletedPublisher_WhenPublisherIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int publisherId = 1;

            Publisher newPublisher = new()
            {
                Id = publisherId,
                Name = "Gyldendal",
            };

            _context.Publisher.Add(newPublisher);
            await _context.SaveChangesAsync();

            // Act
            var result = await _publisherRepository.DeletePublisherWithProcedure(publisherId);
            var Publisher = await _publisherRepository.SelectPublisherById(publisherId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Publisher>(result);
            Assert.Equal(publisherId, result.Id);
            Assert.Null(Publisher);
        }

        [Fact]
        public async void DeletePublisherById_ShouldReturnNull_WhenPublisherDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _publisherRepository.DeletePublisherWithProcedure(1);

            // Assert
            Assert.Null(result);
        }
    }
}
