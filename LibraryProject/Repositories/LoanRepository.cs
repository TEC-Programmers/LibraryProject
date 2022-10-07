using LibraryProject.API.Database.Entities;
using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface ILoanRepository
    {
        Task<List<Loan>> SelectAllLoansWithProcedure();
        Task<Loan> SelectLoanById(int loanId);
        Task<Loan> InsertNewLoanWithProcedure(Loan loan);
        Task<Loan> UpdateExistingLoan(int loanId, Loan loan);
        Task<Loan> DeleteLoanByIdWithProcedure(int loanId);

    }
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryProjectContext _context;
        public string _connectionString;

        public LoanRepository(LibraryProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<Loan> InsertNewLoanWithProcedure(Loan loan)
        {
            //_context.Loan.Add(loan);
            //await _context.SaveChangesAsync();
            //return loan;

            using SqlConnection sql = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("insertLoan", sql);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@userId", loan.userId));
            cmd.Parameters.Add(new SqlParameter("@bookId", loan.bookId));
            cmd.Parameters.Add(new SqlParameter("@loaned_At", loan.loaned_At));
            cmd.Parameters.Add(new SqlParameter("@return_date", loan.return_date));

            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return loan;
        }
        public async Task<Loan> DeleteLoanByIdWithProcedure(int loanId)
        {
            Loan deleteLoan = await _context.Loan
                .FirstOrDefaultAsync(u => u.Id == loanId);

            var parameter = new List<SqlParameter>
            {
               new SqlParameter("@Id", loanId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC deleteLoan @Id", parameter.ToArray());
            return deleteLoan;
        }
        public async Task<List<Loan>> SelectAllLoansWithProcedure()
        {
            return await _context.Loan.FromSqlRaw("selectAllLoans").ToListAsync();
        }
        public async Task<Loan> SelectLoanById(int loanId)
        {
            return await _context.Loan
                .FirstOrDefaultAsync(loan => loan.Id == loanId);
        }
        public async Task<Loan> UpdateExistingLoan(int loanId, Loan loan)
        {
            Loan updateLoan = await _context.Loan.FirstOrDefaultAsync(loan => loan.Id == loanId);
            if (updateLoan != null)
            {
                updateLoan.userId = loan.userId;
                updateLoan.bookId = loan.bookId;
                updateLoan.loaned_At = loan.loaned_At;
                updateLoan.return_date = loan.return_date;

                await _context.SaveChangesAsync();
            }
            return updateLoan;

        }
    }
}
