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
    public class ReservationControllerTests
    {
        private readonly ReservationController _reservationController;
        private readonly Mock<IReservationService> _mockReservationService = new();

        public ReservationControllerTests()
        {
            _reservationController = new(_mockReservationService.Object);
        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenReservationsExists()
        {
            //Arrange - Instancierer klasser og objekter og sætter elementer sammen som skal testes
            List<ReservationResponse> Reservations = new();

            Reservations.Add(new()
            {
                Id = 1,
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            });

            Reservations.Add(new()
            {
                Id = 2,
                userId = 2,
                bookId = 2,
                reserved_At = "11/05/22",
                reserved_To = "27/05/22"
            });

            _mockReservationService
            .Setup(x => x.GetAllReservations())
            .ReturnsAsync(Reservations);

            //Act - kalder metoder og udføre de faktiske tests
            var result = await _reservationController.GetAll();

            //Assert - observer resultat pointer som skal bestås for at testen er successfuld
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoReservationsExists()
        {
            //Arrange
            List<ReservationResponse> reservations = new();

            _mockReservationService
                .Setup(x => x.GetAllReservations())
                .ReturnsAsync(reservations);

            //Act

            var result = await _reservationController.GetAll();

            //Assert

            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //Arrange

            _mockReservationService
                .Setup(x => x.GetAllReservations())
                .ReturnsAsync(() => null);


            //Act
            var result = await _reservationController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockReservationService
                .Setup(x => x.GetAllReservations())
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));


            //Act
            var result = await _reservationController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenReservationIsSuccessfullyCreated()
        {
            //Arrange

            ReservationRequest newRerservation = new()
            {
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            };

            int Id = 1;

            ReservationResponse reservationResponse = new()
            {
                Id = Id,
                userId = 2,
                bookId = 2,
                reserved_At = "11/05/22",
                reserved_To = "27/05/22"
            };

            _mockReservationService
                .Setup(x => x.CreateReservation(It.IsAny<ReservationRequest>()))
                .ReturnsAsync(reservationResponse);

            //Act
            var esult = await _reservationController.Create(newRerservation);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)esult;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            ReservationRequest newRerservation = new()
            {
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            };

            _mockReservationService
                .Setup(x => x.CreateReservation(It.IsAny<ReservationRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _reservationController.Create(newRerservation);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange
            int ReservationId = 1;

            ReservationResponse Reservation = new()
            {
                Id = ReservationId,
                userId = 1,
                bookId = 1,
                reserved_At = "06/05/22",
                reserved_To = "13/05/22"
            };

            _mockReservationService
                .Setup(x => x.GetReservationById(It.IsAny<int>()))
                .ReturnsAsync(Reservation);

            //Act
            var result = await _reservationController.GetById(ReservationId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }

        [Fact]
        public async Task GetById_ShouldReturnStatusCode404_WhenReservationDoesNotExist()
        {
            //Arrange
            int ReservationId = 1;

            _mockReservationService
                .Setup(x => x.GetReservationById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _reservationController.GetById(ReservationId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            _mockReservationService
                .Setup(x => x.GetReservationById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _reservationController.GetById(1);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenReservationIsSuccessfullyUpdated()
        {
            //Arrange

            ReservationRequest updateRerservation = new()
            {
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            };

            int Id = 1;

            ReservationResponse reservationResponse = new()
            {
                Id = Id,
                userId = 2,
                bookId = 2,
                reserved_At = "11/05/22",
                reserved_To = "27/05/22"
            };

            _mockReservationService
                .Setup(x => x.UpdateReservation(It.IsAny<int>(), It.IsAny<ReservationRequest>()))
                .ReturnsAsync(reservationResponse);

            //Act
            var result = await _reservationController.Update(Id,updateRerservation);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenTryingToUpdateReservationWhichDoesNotExist()
        {
            //Arrange

            ReservationRequest updateRerservation = new()
            {
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            };

            int Id = 1;

            _mockReservationService
                .Setup(x => x.UpdateReservation(It.IsAny<int>(), It.IsAny<ReservationRequest>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _reservationController.Update(Id, updateRerservation);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            ReservationRequest updateRerservation = new()
            {
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            };

            int Id = 1;

            _mockReservationService
                .Setup(x => x.UpdateReservation(It.IsAny<int>(), It.IsAny<ReservationRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _reservationController.Update(Id, updateRerservation);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenReservationIsDeleted()
        {
            //Arrange

            int Id = 1;


            ReservationResponse reservationResponse = new()
            {
                Id = Id,
                userId = 2,
                bookId = 2,
                reserved_At = "11/05/22",
                reserved_To = "27/05/22"
            };

            _mockReservationService
                .Setup(x => x.DeleteReservation(It.IsAny<int>()))
                .ReturnsAsync(reservationResponse);

            //Act
            var result = await _reservationController.Delete(Id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTryingToDeleteReservationWhichDoesNotExist()
        {
            //Arrange

            int Id = 1;

            _mockReservationService
                .Setup(x => x.DeleteReservation(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _reservationController.Delete(Id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            int Id = 1;

            _mockReservationService
                .Setup(x => x.DeleteReservation(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("this is an exceoption"));

            //Act
            var result = await _reservationController.Delete(Id);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
