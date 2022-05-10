using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.Repositories
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> SelectAllReservations();
        Task<Reservation> SelectReservationById(int reservationId);
        Task<Reservation> InsertNewReservation(Reservation reservation);
        Task<Reservation> UpdateExistingReservation(int reservationId, Reservation reservation);
        Task<Reservation> DeleteReservationById(int reservationId);

    }
    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryProjectContext _context;

        public ReservationRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<Reservation> DeleteReservationById(int reservationId)
        {
            Reservation deleteReservation = await _context.reservation.FirstOrDefaultAsync(reservation => reservation.Id == reservationId);
            if (deleteReservation != null)
            {
                _context.reservation.Remove(deleteReservation);
                await _context.SaveChangesAsync();
            }

            return deleteReservation;
        }

        public async Task<Reservation> InsertNewReservation(Reservation reservation)
        {
            _context.reservation.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<List<Reservation>> SelectAllReservations()
        {
            return await _context.reservation.ToListAsync();
        }

        public async Task<Reservation> SelectReservationById(int reservationId)
        {
            return await _context.reservation
                .FirstOrDefaultAsync(reservation => reservation.Id == reservationId);
        }

    public async Task<Reservation> UpdateExistingReservation(int reservationId, Reservation reservation)
    {
        Reservation updateReservation = await _context.reservation.FirstOrDefaultAsync(reservation => reservation.Id == reservationId);

        if (updateReservation != null)
        {
            updateReservation.Id = reservation.Id;
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
