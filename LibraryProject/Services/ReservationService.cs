using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{
    public interface IReservationService
    {
        Task<List<ReservationResponse>> GetAllReservations();
        Task<ReservationResponse> GetReservationById(int reservationId);
        Task<ReservationResponse> CreateReservationWithProcedure(ReservationRequest newReservation);
        Task<ReservationResponse> CreateReservation(ReservationRequest newReservation);
        Task<ReservationResponse> UpdateReservation(int reservationId, ReservationRequest updateReservation);
        Task<ReservationResponse> DeleteReservationWithProcedure(int reservationId);
        Task<ReservationResponse> DeleteReservation(int reservationId);

    }
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ReservationResponse> CreateReservationWithProcedure(ReservationRequest newReservation)
        {
            Reservation reservation = new()
            {
                UserId = newReservation.UserId,
                bookId = newReservation.bookId,
                reserved_At = newReservation.reserved_At,
                reserved_To = newReservation.reserved_To
            };


            Reservation insertedReservation = await _reservationRepository.InsertNewReservationWithProcedure(reservation);

            if (insertedReservation != null)
            {
                return new ReservationResponse()
                {
                    Id = insertedReservation.Id,
                    UserId = insertedReservation.UserId,
                    bookId = insertedReservation.bookId,
                    reserved_At = newReservation.reserved_At,
                    reserved_To = newReservation.reserved_To
                };
            }

            return null;

        }

        public async Task<ReservationResponse> CreateReservation(ReservationRequest newReservation)
        {
            Reservation reservation = new()
            {
                UserId = newReservation.UserId,
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
                    UserId = insertedReservation.UserId,
                    bookId = insertedReservation.bookId,
                    reserved_At = newReservation.reserved_At,
                    reserved_To = newReservation.reserved_To
                };
            }

            return null;

        }

        public async Task<ReservationResponse> DeleteReservationWithProcedure(int reservationId)
        {
            Reservation deletedReservation = await _reservationRepository.DeleteReservationByIdWithProcedure(reservationId);

            if (deletedReservation != null)
            {
                return new ReservationResponse()
                {
                    Id = deletedReservation.Id,
                    UserId = deletedReservation.UserId,
                    bookId = deletedReservation.bookId,
                    reserved_At = deletedReservation.reserved_At,
                    reserved_To = deletedReservation.reserved_To
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
                    UserId = deletedReservation.UserId,
                    bookId = deletedReservation.bookId,
                    reserved_At = deletedReservation.reserved_At,
                    reserved_To = deletedReservation.reserved_To
                };
            }
            return null;
        }

        public async Task<List<ReservationResponse>> GetAllReservations()
        {
            List<Reservation> reservations = await _reservationRepository.SelectAllReservationsWithProcedure();
            return reservations.Select(reservation => MapReservationToReservationResponse(reservation)).ToList();           
        }
        public async Task<ReservationResponse> GetReservationById(int reservationId)
        {
            Reservation reservation = await _reservationRepository.SelectReservationById(reservationId);

            if (reservation != null)
            {
                return new ReservationResponse()
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    bookId = reservation.bookId,
                    reserved_At = reservation.reserved_At,
                    reserved_To = reservation.reserved_To
                };
            }
            return null;
        }
        public async Task<ReservationResponse> UpdateReservation(int reservationId, ReservationRequest updateReservation)
        {
            Reservation reservation = new()
            {
                UserId = updateReservation.UserId,
                bookId = updateReservation.bookId,
                reserved_At = updateReservation.reserved_At,
                reserved_To = updateReservation.reserved_To
            };


            Reservation updatedReservation = await _reservationRepository.UpdateReservation(reservationId, reservation);

            if (updatedReservation != null)
            {
                return new ReservationResponse()
                {
                    Id = updatedReservation.Id,
                    UserId = updatedReservation.UserId,
                    bookId = updatedReservation.bookId,
                    reserved_At = updatedReservation.reserved_At,
                    reserved_To = updatedReservation.reserved_To
                };
            }

            return null;
        }
        private Reservation MapReservationRequestToReservation(ReservationRequest ReservationRequest)
        {
            return new Reservation()
            {
                UserId = ReservationRequest.UserId,
                bookId = ReservationRequest.bookId,
                reserved_At = ReservationRequest.reserved_At,
                reserved_To = ReservationRequest.reserved_To
            };
        }
        private ReservationResponse MapReservationToReservationResponse(Reservation Reservation)
        {
            return new ReservationResponse()
            {
                Id = Reservation.Id,
                UserId = Reservation.UserId,
                bookId = Reservation.bookId,
                reserved_At = Reservation.reserved_At,
                reserved_To = Reservation.reserved_To
            };
        }
    }
}
