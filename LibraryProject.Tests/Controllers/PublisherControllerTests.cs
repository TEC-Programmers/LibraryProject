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
    public class PublisherControllerTests
    {
        private readonly PublisherController _publisherController;
        private readonly Mock<IPublisherService> _mockPublisherService = new();

        public PublisherControllerTests()
        {
            _publisherController = new(_mockPublisherService.Object);
        }

        [Fact]
        public async void getAll_ShouldReturnStatusCode200_WhenPublishersExists()
        {
            //  Arrange
            List<PublisherResponse> publishers = new();

            publishers.Add(new()
            {
                Id = 1,
                Name = "Gyldendal"
            }); ;

            publishers.Add(new()
            {
                Id = 2,
                Name = "Rosinante"
            });

            _mockPublisherService
                .Setup(x => x.GetAllPublishers())
                .ReturnsAsync(publishers);

            //  Act
            var result = await _publisherController.getAll();

            //  Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void getAll_ShouldReturnStatusCode204_WhenNoPublishersExists()
        {
            //  Arrange
            List<PublisherResponse> Publishers = new();

            _mockPublisherService
                .Setup(x => x.GetAllPublishers())
                .ReturnsAsync(Publishers);

            //  Act
            var result = await _publisherController.getAll();

            //  Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void getAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //  Arrange
            _mockPublisherService
                .Setup(x => x.GetAllPublishers())
                .ReturnsAsync(() => null);

            //  Act
            var result = await _publisherController.getAll();

            //  Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void getAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //  Arrange
            _mockPublisherService
                .Setup(x => x.GetAllPublishers())
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            //  Act
            var result = await _publisherController.getAll();

            //  Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange
            int publisherId = 1;

            PublisherResponse Publisher = new()
            {
                Id = publisherId,
                Name = "Gyldendal"
            };

            _mockPublisherService
                .Setup(x => x.GetPublisherById(It.IsAny<int>()))
                .ReturnsAsync(Publisher);

            //Act
            var result = await _publisherController.GetById(publisherId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async Task GetById_ShouldReturnStatusCode404_WhenPublisherDoesNotExist()
        {
            //Arrange
            int publisherId = 1;

            _mockPublisherService
                .Setup(x => x.GetPublisherById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _publisherController.GetById(publisherId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            _mockPublisherService
                .Setup(x => x.GetPublisherById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _publisherController.GetById(1);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturnStatusCode200_WHenPublisherIsSuccessfullyCreated()
        {
            //Arrange
            PublisherRequest newPublisher = new()
            {
                Name = "Gyldendal"
            };

            int publisherId = 1;

            PublisherResponse PublisherResponse = new()
            {
                Id = publisherId,
                Name = "Gyldendal"
            };

            _mockPublisherService
                .Setup(x => x.CreatePublisher(It.IsAny<PublisherRequest>()))
                .ReturnsAsync(PublisherResponse);

            //Act
            var result = await _publisherController.Create(newPublisher);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExcetionIsRaised()
        {
            //Arrange
            PublisherRequest newPublisher = new()
            {
                Name = "Gyldendal"
            };

            _mockPublisherService
                .Setup(x => x.CreatePublisher(It.IsAny<PublisherRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _publisherController.Create(newPublisher);


            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenPublisherIsSuccessfullyUpdated()
        {
            //Arrange
            PublisherRequest updatePublisher = new()
            {
                Name = "Gyldendal"
            };

            int publisherId = 1;

            PublisherResponse PublisherResponse = new()
            {
                Id = publisherId,
                Name = "Gyldendal"
            };

            _mockPublisherService
                .Setup(x => x.UpdatePublisher(It.IsAny<int>(), It.IsAny<PublisherRequest>()))
                .ReturnsAsync(PublisherResponse);

            //Act
            var result = await _publisherController.Update(publisherId, updatePublisher);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenTryingToUpdatePublisherWhichDoesNotExist()
        {
            //Arrange
            PublisherRequest updatePublisher = new()
            {
                Name = "Gyldendal"
            };

            int publisherId = 1;

            _mockPublisherService
                .Setup(x => x.UpdatePublisher(It.IsAny<int>(), It.IsAny<PublisherRequest>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _publisherController.Update(publisherId, updatePublisher);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            PublisherRequest updatePublisher = new()
            {
                Name = "Gyldendal"
            };

            int publisherId = 1;

            _mockPublisherService
                .Setup(x => x.UpdatePublisher(It.IsAny<int>(), It.IsAny<PublisherRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _publisherController.Update(publisherId, updatePublisher);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenPublisherIsDeleted()
        {
            //Arrange
            int publisherId = 1;

            PublisherResponse PublisherResponse = new()
            {
                Id = publisherId,
                Name = "Gyldendal"
            };

            _mockPublisherService
                .Setup(x => x.DeletePublisher(It.IsAny<int>()))
                .ReturnsAsync(PublisherResponse);

            //Act
            var result = await _publisherController.Delete(publisherId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTryingToDeletePublisherWhichDoesNotExist()
        {
            //Arrange
            int publisherId = 1;

            _mockPublisherService
                .Setup(x => x.DeletePublisher(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _publisherController.Delete(publisherId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int publisherId = 1;

            _mockPublisherService
                .Setup(x => x.DeletePublisher(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _publisherController.Delete(publisherId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
