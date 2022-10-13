using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using LibraryProject.API.Services;
using LibraryProject.Database;
using LibraryProject.Database.Entities;
using LibraryProject.DTO_s;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Services
{
    public class ReservationServiceTests
    {

        private readonly ReservationService _reservationService;
        private readonly Mock<IReservationRepository> _mockReservationRepository = new();

        public ReservationServiceTests()
        {
            _reservationService = new ReservationService(_mockReservationRepository.Object);
        }


        [Fact]
        public async void GetAllReservations_ShouldReturnListofReservationResponses_WhenReservationsExists()
        {
            //Arrange - Instancierer klasser og objekter og sætter elementer sammen som skal testes
            List<Reservation> Reservations = new();


            Reservations.Add(new()
            {
                Id = 1,
                UsersId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            });

            Reservations.Add(new()
            {
                Id = 2,
                UsersId = 2,
                bookId = 2,
                reserved_At = "11/05/22",
                reserved_To = "27/05/22"
            });

             _mockReservationRepository
              .Setup(x => x.SelectAllReservations())
              .ReturnsAsync(Reservations);

            //Act - kalder metoder og udføre de faktiske tests
            var result = await _reservationService.GetAllReservations();

            //Assert - observer resultat pointer som skal bestås for at testen er successfuld
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<ReservationResponse>>(result);
        }

        [Fact]
        public async void GetAllReservations_ShouldReturnEmptyListOfReservationResponses_WhenNoReservationsExists()
        {
            //Arrange
            List<Reservation> Reservations = new();

            _mockReservationRepository
                .Setup(x => x.SelectAllReservations())
                .ReturnsAsync(Reservations);

            //Act
            var result = await _reservationService.GetAllReservations();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<ReservationResponse>>(result);
        }



        [Fact]
        public async void GetReservationById_ShouldReturnReservationResponses_WhenReservationExists()
        {
            //Arrange
            int reservationId = 1;

            Reservation reservation = new()
            {
                Id = reservationId,
                UsersId = 1,
                bookId = 1,
                reserved_At = "10/05/22",
                reserved_To = "12/06/22"
            };

            _mockReservationRepository
                .Setup(x => x.SelectReservationById(It.IsAny<int>()))
                .ReturnsAsync(reservation);

            //Act
            var result = await _reservationService.GetReservationById(reservationId);

            //Asser
            Assert.NotNull(result);
            Assert.IsType<ReservationResponse>(result);
            Assert.Equal(reservation.Id,result.Id);
            Assert.Equal(reservation.UsersId,result.UsersId);
            Assert.Equal(reservation.bookId,result.bookId);
            Assert.Equal(reservation.reserved_At, result.reserved_At);
            Assert.Equal(reservation.reserved_To, result.reserved_To);
        }

        [Fact]
        public async void GetReservationById_ShouldReturnNull_WhenReservationDoesNotExist()
        {
            //Arrange
            int reservationId = 1;

            _mockReservationRepository
                .Setup(x => x.SelectReservationById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _reservationService.GetReservationById(reservationId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateReservation_ShouldReturnReservationResponse_WhenCreateIsSuccess()
        {
            ReservationRequest newReservation = new()
            {
                UsersId = 1,
                bookId = 1,
                reserved_At = "10/05/22",
                reserved_To = "12/06/22"
            };

            int reservationId = 1;

            Reservation createdReservation = new()
            {
                Id = reservationId,
                UsersId = 1,
                bookId = 1,
                reserved_At = "10/05/22",
                reserved_To = "12/06/22"
            };

            _mockReservationRepository
                .Setup(x=> x.InsertNewReservation(It.IsAny<Reservation>()))
                .ReturnsAsync(createdReservation);

            //Act
            var result = await _reservationService.CreateReservation(newReservation);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ReservationResponse>(result);
            Assert.Equal(reservationId, result.Id);
            Assert.Equal(newReservation.UsersId, result.UsersId);
            Assert.Equal(newReservation.bookId, result.bookId);
            Assert.Equal(newReservation.reserved_At, result.reserved_At);
            Assert.Equal(newReservation.reserved_To, result.reserved_To);

        }

        [Fact]
        public async void CreateReservation_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            //Arrange
            ReservationRequest newReservation = new()
            {
                UsersId = 1,
                bookId = 1,
                reserved_At = "10/05/22",
                reserved_To = "12/06/22"
            };

            _mockReservationRepository
                 .Setup(x => x.InsertNewReservation(It.IsAny<Reservation>()))
                 .ReturnsAsync(() => null);

            //Act
            var result = await _reservationService.CreateReservation(newReservation);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateReservation_ShouldReturnReservationResponse_WhenUpdateIsSuccess()
        {
            //Notice we do not test if anything actually changed on the DB,
            //we only test that the returned values match the submitted values

            //Arrange

            ReservationRequest ReservationRequest = new()
            {
                UsersId = 1,
                bookId = 1,
                reserved_At = "10/05/22",
                reserved_To = "12/06/22"
            };

            int reservationId = 1;

            Reservation reservation = new()
            {
                Id = reservationId,
                UsersId = 1,
                bookId = 1,
                reserved_At = "10/05/22",
                reserved_To = "12/06/22"
            };

            _mockReservationRepository
                .Setup(x => x.UpdateReservation(It.IsAny<int>(), It.IsAny<Reservation>()))
                .ReturnsAsync(reservation);

            //Act
            var result = await _reservationService.UpdateReservation(reservationId, ReservationRequest);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<ReservationResponse>(result);
            Assert.Equal(reservationId, result.Id);
            Assert.Equal(ReservationRequest.UsersId, result.UsersId);
            Assert.Equal(ReservationRequest.bookId, result.bookId);
            Assert.Equal(ReservationRequest.reserved_At, result.reserved_At);
            Assert.Equal(ReservationRequest.reserved_To, result.reserved_To);
        }

        [Fact]
        public async void UpdateReservation_ShouldReturnNull_WhenReservationDoesNotExist()
        {
            //Arrange
            ReservationRequest ReservationRequest = new()
            {
                UsersId = 1,
                bookId = 1,
                reserved_At = "10/05/22",
                reserved_To = "12/06/22"
            };

            int reservationId = 1;


            _mockReservationRepository
                .Setup(x => x.UpdateReservation(It.IsAny<int>(), It.IsAny<Reservation>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _reservationService.UpdateReservation(reservationId, ReservationRequest);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteReservation_ShouldReturnReservationResponse_WhenReservationIsSuccess()
        {
            int reservationId = 1;

            Reservation deletedReservation = new()
            {
                Id = 1,
                UsersId = 1,
                bookId = 1,
                reserved_At = "10/05/22",
                reserved_To = "12/06/22"
            };

            _mockReservationRepository
                   .Setup(x => x.DeleteReservationById(It.IsAny<int>()))
                   .ReturnsAsync(deletedReservation);

            //Act
            var result = await _reservationService.DeleteReservation(reservationId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ReservationResponse>(result);
            Assert.Equal(reservationId, result.Id);
        }

        [Fact]
        public async void DeleteReservation_ShouldReturnNull_WhenReservationDoesNotExist()
        {
            //Arrange
            int reservationId = 1;

            _mockReservationRepository
                  .Setup(x => x.DeleteReservationById(It.IsAny<int>()))
                  .ReturnsAsync(() => null);

            //Act
            var result = await _reservationService.DeleteReservation(reservationId);

            //Assert
            Assert.Null(result);
        }
    }
}
