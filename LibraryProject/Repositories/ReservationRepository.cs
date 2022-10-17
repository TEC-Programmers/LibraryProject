using LibraryProject.API.Database.Entities;
using LibraryProject.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using LibraryProject.Database.Entities;

namespace LibraryProject.API.Repositories
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> SelectAllReservationsWithProcedure();
        Task<Reservation> SelectReservationById(int reservationId);
        Task<Reservation> InsertNewReservationWithProcedure(Reservation reservation);
        Task<Reservation> InsertNewReservation(Reservation reservation);
        Task<Reservation> UpdateReservation(int reservationId, Reservation reservation);
        Task<Reservation> DeleteReservationByIdWithProcedure(int reservationId);
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
            Reservation deleteReservation = await _context.Reservation
               .FirstOrDefaultAsync(reservation => reservation.Id == reservationId);
            if (deleteReservation != null)
            {
                _context.Reservation.Remove(deleteReservation);
                await _context.SaveChangesAsync();
            }
            return deleteReservation;
        }
        public async Task<Reservation> DeleteReservationByIdWithProcedure(int reservationId)
        {
            Reservation deleteReservation = await _context.Reservation
               .FirstOrDefaultAsync(u => u.Id == reservationId);

            var parameter = new List<SqlParameter>
            {
               new SqlParameter("@Id", reservationId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC deleteReservation @Id", parameter.ToArray());
            return deleteReservation;
        }
        public async Task<Reservation> InsertNewReservationWithProcedure(Reservation reservation)
        {
            var UserId = new SqlParameter("@UserId", reservation.UserId);
            var bookId = new SqlParameter("@bookId", reservation.bookId);
            var reservationed_At = new SqlParameter("@reservationed_At", reservation.reserved_At);
            var return_date = new SqlParameter("@return_date", reservation.reserved_To);

            await _context.Database.ExecuteSqlRawAsync("exec insertReservation @UserId, @bookId, @reservationed_At, @return_date", UserId, bookId, reservationed_At, return_date);
            return reservation;           
        }
        public async Task<Reservation> InsertNewReservation(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }
        public async Task<List<Reservation>> SelectAllReservationsWithProcedure()
        {
            return await _context.Reservation.FromSqlRaw("selectAllReservations").ToListAsync();
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
                updateReservation.UserId = reservation.UserId;
                updateReservation.bookId = reservation.bookId;
                updateReservation.reserved_At = reservation.reserved_At;
                updateReservation.reserved_To = reservation.reserved_To;

                await _context.SaveChangesAsync();
            }
            return updateReservation;
        }
    }
}
