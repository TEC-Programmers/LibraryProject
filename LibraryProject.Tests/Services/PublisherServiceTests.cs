using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using LibraryProject.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Services
{
    public class PublisherServiceTests
    {
        private readonly PublisherService _publisherService;
        private readonly Mock<IPublisherRepository> _mockPublisherRepository = new();

        public PublisherServiceTests()
        {
            _publisherService = new PublisherService(_mockPublisherRepository.Object);
        }

        [Fact]
        public async void GetAllPublishers_ShouldReturnListOfPublisherResponses_WhenPublishersExists()
        {
            // Arrange 
            List<Publisher> publishers = new();

            publishers.Add(new()
            {
                Id = 1,
                Name = "Gyldendal"             
            });

            publishers.Add(new()
            {
                Id = 2,
                Name = "Rosinante"
            });

            _mockPublisherRepository
                .Setup(x => x.SelectAllPublishers())
                .ReturnsAsync(publishers);

            // Act
            var result = await _publisherService.GetAllPublishers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<PublisherResponse>>(result);
        }

        [Fact]
        public async void GetAllPublishers_ShouldReturnEmptyListOfPublisherResponses_WhenNoPublishersExists()
        {
            // Arrange 
            List<Publisher> publishers = new();

            _mockPublisherRepository
                .Setup(x => x.SelectAllPublishers())
                .ReturnsAsync(publishers);

            // Act
            var result = await _publisherService.GetAllPublishers();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<PublisherResponse>>(result);
        }

        [Fact]
        public async void GetPublishersById_ShouldReturnPublisherResponse_WhenPublisherExists()
        {
            // Arrange 
            int publisherId = 1;

            Publisher Publisher = new()
            {
                Id = publisherId,
                Name = "Gyldendal"
            };

            _mockPublisherRepository
                .Setup(x => x.SelectPublisherById(It.IsAny<int>()))
                .ReturnsAsync(Publisher);

            // Act
            var result = await _publisherService.GetPublisherById(publisherId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PublisherResponse>(result);
            Assert.Equal(publisherId, result.Id);
            Assert.Equal(Publisher.Name, result.Name);
        }

        [Fact]
        public async void GetPublishersById_ShouldReturnNull_WhenPublisherDoesNotExists()
        {
            // Arrange 
            int publisherId = 1;

            _mockPublisherRepository
                .Setup(x => x.SelectPublisherById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _publisherService.GetPublisherById(publisherId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreatePublisher_ShouldReturnPublisherResponse_WhenCreateIsSuccess()
        {
            // Arrange 
            PublisherRequest newPublisher = new()
            {
                Name = "Gyldendal"
            };

            int publisherId = 1;

            Publisher createdPublisher = new()
            {
                Id = publisherId,
                Name = "Gyldendal"
            };

            _mockPublisherRepository
                .Setup(x => x.InsertNewPublisher(It.IsAny<Publisher>()))
                .ReturnsAsync(createdPublisher);

            // Act
            var result = await _publisherService.CreatePublisher(newPublisher);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PublisherResponse>(result);
            Assert.Equal(publisherId, result.Id);
            Assert.Equal(newPublisher.Name, result.Name);
        }

        [Fact]
        public async void CreatePublisher_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            // Arrange 
            PublisherRequest newPublisher = new()
            {
                Name = "Gyldendal"
            };

            _mockPublisherRepository
                .Setup(x => x.InsertNewPublisher(It.IsAny<Publisher>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _publisherService.CreatePublisher(newPublisher);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdatePublisher_ShouldReturnPublisherResponse_WhenUpdateIsSuccess()
        {
            // Arrange 
            PublisherRequest PublisherRequest = new()
            {
                Name = "Gyldendal"
            };

            int publisherId = 1;

            Publisher Publisher = new()
            {
                Id = publisherId,
                Name = "Gyldendal"
            };

            _mockPublisherRepository
                .Setup(x => x.UpdateExistingPublisher(It.IsAny<int>(), It.IsAny<Publisher>()))
                .ReturnsAsync(Publisher);

            // Act
            var result = await _publisherService.UpdatePublisher(publisherId, PublisherRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PublisherResponse>(result);
            Assert.Equal(publisherId, result.Id);
            Assert.Equal(PublisherRequest.Name, result.Name);
        }

        [Fact]
        public async void UpdatePublisher_ShouldReturnNull_WhenPublisherDoesNotExist()
        {
            // Arrange 
            PublisherRequest PublisherRequest = new()
            {
                Name = "Gyldendal"
            };

            int publisherId = 1;

            _mockPublisherRepository
                .Setup(x => x.UpdateExistingPublisher(It.IsAny<int>(), It.IsAny<Publisher>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _publisherService.UpdatePublisher(publisherId, PublisherRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeletePublisher_ShouldReturnPublisherResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int publisherId = 1;

            Publisher deletedPublisher = new()
            {
                Id = 1,
                Name = "Gyldendal"
            };

            _mockPublisherRepository
                .Setup(x => x.DeletePublisher(It.IsAny<int>()))
                .ReturnsAsync(deletedPublisher);

            // Act
            var result = await _publisherService.DeletePublisher(publisherId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PublisherResponse>(result);
            Assert.Equal(publisherId, result.Id);
        }

        [Fact]
        public async void DeletePublisher_ShouldReturnNull_WhenPublisherDoesNotExist()
        {
            // Arrange
            int publisherId = 1;

            _mockPublisherRepository
                .Setup(x => x.DeletePublisher(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _publisherService.DeletePublisher(publisherId);

            // Assert
            Assert.Null(result);
        }
    }
}
