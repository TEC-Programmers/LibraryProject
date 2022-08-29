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
        Task<Reservation> DeleteReservationById(int reservationId);

    }
    public class ReservationRepository : IReservationRepository      // This class is inheriting interfcae ILoanRepository and implement the interfaces
    {
        private readonly LibraryProjectContext _context;                //making an instance of the class LibraryProjectContext

        public ReservationRepository(LibraryProjectContext context)         //dependency injection with parameter 
        {
            _context = context;
        }

        //**implementing the methods of IAuthorRepository interface**// 

        //This method will remove one specific Reservation whose Id has been got
        public async Task<Reservation> DeleteReservationById(int reservationId)
        {
            Reservation deleteReservation = await _context.Reservation.FirstOrDefaultAsync(reservation => reservation.Id == reservationId);
            if (deleteReservation != null)
            {
                _context.Reservation.Remove(deleteReservation);
                await _context.SaveChangesAsync();
            }

            return deleteReservation;
        }

        //This method will add a new Reservation to the system
        public async Task<Reservation> InsertNewReservation(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        //this method will get all Reservations details
        public async Task<List<Reservation>> SelectAllReservations()
        {
            return await _context.Reservation.ToListAsync();
        }

        //this method will get info of one Reservation by specific ID
        public async Task<Reservation> SelectReservationById(int reservationId)
        {
            return await _context.Reservation
                .FirstOrDefaultAsync(reservation => reservation.Id == reservationId);
        }

        //This method will update the information of the specific Publisher by ID

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
