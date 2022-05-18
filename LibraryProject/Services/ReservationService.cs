using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using LibraryProject.Database.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{
    public interface IReservationService
    {
        Task<List<ReservationResponse>> GetAllReservations();
        Task<ReservationResponse> GetReservationById(int reservationId);
        Task<ReservationResponse> CreateReservation(ReservationRequest newReservation);
        Task<ReservationResponse> UpdateExistingReservation(int reservationId, ReservationRequest updateReservation);
        Task<ReservationResponse> DeleteReservation(int reservationId);
    }
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ReservationResponse> CreateReservation(ReservationRequest newReservation)
        {
            Reservation reservation = new()
            {
                userId = newReservation.userId,
                bookId = newReservation.bookId,
                reserved_At = newReservation.reserved_At,
                reserved_To = newReservation.reserved_To
            };


            Reservation insertedReservation = await _reservationRepository.InsertNewReservation(reservation);

            if (insertedReservation != null)
            {
                return new ReservationResponse()
                {
                    Id = insertedReservation.Id,
                    userId = insertedReservation.userId,
                    bookId = insertedReservation.bookId,
                    reserved_At = newReservation.reserved_At,
                    reserved_To = newReservation.reserved_To
                };
            }

            return null;

        }

        public async Task<ReservationResponse> DeleteReservation(int reservationId)
        {
            Reservation deletedReservation = await _reservationRepository.DeleteReservationById(reservationId);

            if (deletedReservation != null)
            {
                return new ReservationResponse()
                {
                    Id = deletedReservation.Id,
                    userId = deletedReservation.userId,
                    bookId = deletedReservation.bookId,
                    reserved_At = deletedReservation.reserved_At,
                    reserved_To = deletedReservation.reserved_To
                };
            }
            return null;
        }

        public async Task<List<ReservationResponse>> GetAllReservations()
        {
            List<Reservation> reservations = await _reservationRepository.SelectAllReservations();

            return reservations.Select(reservation => new ReservationResponse
            {
                Id = reservation.Id,
                userId = reservation.userId,
                bookId = reservation.bookId,
                reserved_At = reservation.reserved_At,
                reserved_To = reservation.reserved_To
            }).ToList();
        }

        public async Task<ReservationResponse> GetReservationById(int reservationId)
        {
            Reservation reservation = await _reservationRepository.SelectReservationById(reservationId);

            if (reservation != null)
            {
                return new ReservationResponse()
                {
                    Id = reservation.Id,
                    userId = reservation.userId,
                    bookId = reservation.bookId,
                    reserved_At = reservation.reserved_At,
                    reserved_To = reservation.reserved_To
                };
            }
            return null;
        }

        public async Task<ReservationResponse> UpdateExistingReservation(int reservationId, ReservationRequest updateReservation)
        {
            Reservation reservation = new()
            {
                userId = updateReservation.userId,
                bookId = updateReservation.bookId,
                reserved_At = updateReservation.reserved_At,
                reserved_To = updateReservation.reserved_To
            };


            Reservation updatedReservation = await _reservationRepository.UpdateExistingReservation(reservationId, reservation);

            if (updatedReservation != null)
            {
                return new ReservationResponse()
                {
                    Id = updatedReservation.Id,
                    userId = updatedReservation.userId,
                    bookId = updatedReservation.bookId,
                    reserved_At = updatedReservation.reserved_At,
                    reserved_To = updatedReservation.reserved_To
                };
            }

            return null;
        }
    }
}
