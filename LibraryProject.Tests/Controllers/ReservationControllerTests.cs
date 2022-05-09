using LibraryProject.Controllers;
using LibraryProject.DTO_s;
using LibraryProject.Services;
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
                reservationId = 1,
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            });

            Reservations.Add(new()
            {
                reservationId = 2,
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
    }
}
