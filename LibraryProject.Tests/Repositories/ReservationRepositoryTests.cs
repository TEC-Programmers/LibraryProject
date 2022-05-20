﻿using LibraryProject.API.Database.Entities;
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
    public class ReservationRepositoryTests
    {
        private readonly DbContextOptions<LibraryProjectContext> _options;
        private readonly LibraryProjectContext _context;
        private readonly ReservationRepository _ReservationRepository;

        public ReservationRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProjectReservations")
                .Options;

            _context = new(_options);

            _ReservationRepository = new(_context);
        }

        [Fact]
        public async void SelectAllReservations_ShouldReturnListOfReservations_WhenReservationsExists()
        {

            //Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Reservation.Add(new()
            {
                Id = 1,
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            });

            _context.Reservation.Add(new()
            {
                Id = 2,
                userId = 2,
                bookId = 2,
                reserved_At = "11/05/22",
                reserved_To = "27/05/22"
            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _ReservationRepository.SelectAllReservations();

            //Assert

            Assert.NotNull(result);
            Assert.IsType<List<Reservation>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void SelectAllReservations_ShouldReturnEmptyListOfReservations_WhenNoReservationsExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _ReservationRepository.SelectAllReservations();

            //Assert

            Assert.NotNull(result);
            Assert.IsType<List<Reservation>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectReservationById_ShouldReturnReservation_WhenReservationExists()
        {

            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int ReservationId = 1;

            _context.Reservation.Add(new()
            {
                Id = ReservationId,
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _ReservationRepository.SelectReservationById(ReservationId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Reservation>(result);
            Assert.Equal(ReservationId, result.Id);
        }


        [Fact]
        public async void SelectReservationById_ShouldReturnNull_WhenReservationDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _ReservationRepository.SelectReservationById(1);

            //Assert
            Assert.Null(result);
        }


        [Fact]
        public async void InsertNewReservation_ShouldReturnNewIdToReservation_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedId = 1;

            Reservation Reservation = new()
            {
                Id = 1,
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            };


            //Act
            var result = await _ReservationRepository.InsertNewReservation(Reservation);

            //Assert

            Assert.NotNull(result);
            Assert.IsType<Reservation>(result);
            Assert.Equal(expectedId, result.Id);
        }

        [Fact]
        public async void InsertNewReservation_ShouldFailToAddNewReservation_WhenReservationAlreadyExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();


            Reservation Reservation = new()
            {
                Id = 1,
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            };

            _context.Reservation.Add(Reservation);
            await _context.SaveChangesAsync();

            //Act
            async Task action() => await _ReservationRepository.InsertNewReservation(Reservation);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added.", ex.Message);
        }

        [Fact]
        public async void Update_ExistingReservation_ShouldCHangeValuesOnReservation_WhenReservationExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int ReservationId = 1;

            Reservation Reservation = new()
            {
                Id = ReservationId,
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            };

            _context.Reservation.Add(Reservation);
            await _context.SaveChangesAsync();

            Reservation updateReservation = new()
            {
                Id = ReservationId,
                userId = 2,
                bookId = 2,
                reserved_At = "11/05/22",
                reserved_To = "27/05/22"
            };

            //Act
            var result = await _ReservationRepository.UpdateReservation(ReservationId, updateReservation);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Reservation>(result);
            Assert.Equal(ReservationId, result.Id);
            Assert.Equal(updateReservation.userId, result.userId);
            Assert.Equal(updateReservation.bookId, result.bookId);
            Assert.Equal(updateReservation.reserved_At, result.reserved_At);
            Assert.Equal(updateReservation.reserved_To, result.reserved_To);

        }


        [Fact]
        public async void Update_ExistingReservation_ShouldReturnNull_WhenReservationDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int ReservationId = 1;

            Reservation updatedReservation = new()
            {
                Id = ReservationId,
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            };




        //Act
        var result = await _ReservationRepository.UpdateReservation(ReservationId, updatedReservation);

        //Assert
        Assert.Null(result);
        }


        [Fact]
        public async void DeleteReservationById_ShouldReturnDeletedReservation_WhenReservationIsDeleted()
        {
            //Arrange

            await _context.Database.EnsureDeletedAsync();

            int ReservationId = 1;

            Reservation newReservation = new()
            {
                Id = ReservationId,
                userId = 1,
                bookId = 1,
                reserved_At = "09/05/22",
                reserved_To = "25/05/22"
            };

            _context.Reservation.Add(newReservation);
            await _context.SaveChangesAsync();

            //Act
            var result = await _ReservationRepository.DeleteReservationById(ReservationId);
            var Reservation = await _ReservationRepository.SelectReservationById(ReservationId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Reservation>(result);
            Assert.Equal(ReservationId, result.Id);
            Assert.Null(Reservation);
        }


        [Fact]
        public async void DeleteReservationById_ShouldReturnNull_WhenReservationDoesNotExist()
        {
            //Arrange
           await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _ReservationRepository.DeleteReservationById(1);

            //Assert
            Assert.Null(result);
        }




    }
}
