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
        Task<ReservationResponse> DeleteReservationById(int reservationId);
    }
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }


        //----------------CREATE------------------//
        public async Task<ReservationResponse> CreateReservation(ReservationRequest newReservation)
        {
            Reservation reservation = MapReservationToRequestReservation(newReservation);

            Reservation insertedReservation = await _reservationRepository.InsertNewReservation(reservation);

            if (insertedReservation != null)
            {
                return MapReservationToReservationResponse(insertedReservation);
            }

            return null;

        }



        //----------------DELETE------------------//

        public async Task<ReservationResponse> DeleteReservationById(int reservationId)
        {
            Reservation deletedReservation = await _reservationRepository.DeleteReservationById(reservationId);

            if (deletedReservation != null)
            {
                return MapReservationToReservationResponse(deletedReservation);
            }
            return null;
        }


        //----------------GETALL------------------//

        public async Task<List<ReservationResponse>> GetAllReservations()
        {
            List<Reservation> reservations = await _reservationRepository.SelectAllReservations();

            return reservations.Select(reservation => MapReservationToReservationResponse(reservation)).ToList();
        }



        //----------------GET BY ID------------------//

        public async Task<ReservationResponse> GetReservationById(int reservationId)
        {
            Reservation reservation = await _reservationRepository.SelectReservationById(reservationId);

            if (reservation != null)
            {
                return
                    MapReservationToReservationResponse(reservation);
            }
            return null;
        }


        //----------------UPDATE------------------//
        public async Task<ReservationResponse> UpdateExistingReservation(int reservationId, ReservationRequest updateReservation)
        {
            Reservation reservation = MapReservationToRequestReservation(updateReservation);


            Reservation updatedReservation = await _reservationRepository.UpdateExistingReservation(reservationId, reservation);

            if (updatedReservation != null)
            {
                return MapReservationToReservationResponse(updatedReservation);
            }

            return null;
        }


        //----------------REQUEST-MAPPING------------------//

        private Reservation MapReservationToRequestReservation(ReservationRequest reservationRequest)
        {
            return new Reservation()
            {
                userId = reservationRequest.userId,
                bookId = reservationRequest.bookId,
                reserved_At = reservationRequest.reserved_At,
                reserved_To = reservationRequest.reserved_To
            };
        }



        //----------------RESPONSE-MAPPING------------------//

        private ReservationResponse MapReservationToReservationResponse(Reservation reservation)
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
    }
}
