using LibraryProject.API.Database.Entities;
using LibraryProject.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> SelectAllReservationsWithProcedure();
        Task<Reservation> SelectReservationById(int reservationId);
        Task<Reservation> InsertNewReservationWithProcedure(Reservation reservation);
        Task<Reservation> UpdateReservation(int reservationId, Reservation reservation);
        Task<Reservation> DeleteReservationByIdWithProcedure(int reservationId);

    }
    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryProjectContext _context;
        public string _connectionString;


        public ReservationRepository(LibraryProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("Default");
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
            //_context.Reservation.Add(reservation);
            //await _context.SaveChangesAsync();
            //return reservation;

            using SqlConnection sql = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("insertReservation", sql);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@userId", reservation.userId));
            cmd.Parameters.Add(new SqlParameter("@bookId", reservation.bookId));
            cmd.Parameters.Add(new SqlParameter("@reserved_At", reservation.reserved_At));
            cmd.Parameters.Add(new SqlParameter("@reserved_To", reservation.reserved_To));

            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
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
