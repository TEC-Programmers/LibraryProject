using LibraryProject.API.Database.Entities;
using LibraryProject.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> SelectAllReservations();
        Task<Reservation> SelectReservationById(int reservationId);
        Task<Reservation> InsertNewReservation(Reservation reservation);
        Task<Reservation> UpdateReservation(int reservationId, Reservation reservation);
        Task<Reservation> DeleteReservation(int reservationId);

    }
    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryProjectContext _context;

        public ReservationRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<Reservation> DeleteReservation(int reservationId)
        {
            Reservation deleteReservation = await _context.Reservation.FirstOrDefaultAsync(reservation => reservation.Id == reservationId);
            if (deleteReservation != null)
            {
                _context.Reservation.Remove(deleteReservation);
                await _context.SaveChangesAsync();
            }

            return deleteReservation;
        }

        public async Task<Reservation> InsertNewReservation(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<List<Reservation>> SelectAllReservations()
        {
            return await _context.Reservation.ToListAsync();
        }

        public async Task<Reservation> SelectReservationById(int reservationId)
        {
            return await _context.Reservation
                .FirstOrDefaultAsync(reservation => reservation.Id == reservationId);
        }

        public async Task<Reservation> UpdateReservation(int reservationId, Reservation reservation)
        {
            Reservation updateReservation = await _context.Reservation.FirstOrDefaultAsync(reservation => reservation.Id == reservationId);

            if (updateReservation != null)
            {
                updateReservation.userId = reservation.userId;
                updateReservation.bookId = reservation.bookId;
                updateReservation.reserved_At = reservation.reserved_At;
                updateReservation.reserved_To = reservation.reserved_To;

                await _context.SaveChangesAsync();
            }
            return updateReservation;
        }
    }
}
