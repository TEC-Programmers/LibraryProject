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
        Task<Loan> InsertNewLoan(Loan loan);
        Task<Loan> UpdateExistingLoan(int loanId, Loan loan);
        Task<Loan> DeleteLoanByIdWithProcedure(int loanId);
        Task<Loan> DeleteLoanById(int loanId);


    }
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryProjectContext _context;

        public LoanRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<Loan> InsertNewLoan(Loan loan)
        {
            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();

            return loan;
        }
        public async Task<Loan> InsertNewLoanWithProcedure(Loan loan)
        {
            var UserId = new SqlParameter("@UserId", loan.UserId);
            var bookId = new SqlParameter("@bookId", loan.bookId);
            var loaned_At = new SqlParameter("@loaned_At", loan.loaned_At);
            var return_date = new SqlParameter("@return_date", loan.return_date);

            await _context.Database.ExecuteSqlRawAsync("exec insertLoan @UserId, @bookId, @loaned_At, @return_date", UserId, bookId, loaned_At, return_date);
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
        public async Task<Loan> DeleteLoanById(int loanId)
        {
            Loan deleteLoan = await _context.Loan
               .FirstOrDefaultAsync(Loan => Loan.Id == loanId);
            if (deleteLoan != null)
            {
                _context.Loan.Remove(deleteLoan);
                await _context.SaveChangesAsync();
            }
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
                updateLoan.UserId = loan.UserId;
                updateLoan.bookId = loan.bookId;
                updateLoan.loaned_At = loan.loaned_At;
                updateLoan.return_date = loan.return_date;

                await _context.SaveChangesAsync();
            }
            return updateLoan;

        }

    }
}
