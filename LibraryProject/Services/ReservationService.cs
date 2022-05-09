using LibraryProject.Database.Entities;
using LibraryProject.DTO_s;
using LibraryProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.Services
{
        public interface IReservationService
        {
            Task<List<ReservationResponse>> GetAllReservations();
        }

        public class ReservationService : IReservationService
        {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

            public async Task<List<ReservationResponse>> GetAllReservations()
            {
                List<Reservation> reservations = await _reservationRepository.SelectAllReservations();

                return reservations.Select(reservation => new ReservationResponse
                {
                    reservationId = reservation.reservationId,
                    userId = reservation.userId,
                    bookId = reservation.bookId,
                    reserved_At = reservation.reserved_At,
                    reserved_To = reservation.reserved_To
                }).ToList();
            }
        }
    
}
