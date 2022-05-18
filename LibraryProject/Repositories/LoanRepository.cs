using LibraryProject.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LibraryProject.API.Repositories
{

    public interface ILoanRepository
    {
        Task<List<Loan>> SelectAllLoans();
        Task<Loan> SelectLoanById(int loanId);
        Task<Loan> InsertNewLoan(Loan loan);
        Task<Loan> UpdateExistingLoan(int loanId, Loan loan);
        Task<Loan> DeleteLoanById(int loanId);

    }
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryProjectContext _context;
        public LoanRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<List<Loan>> SelectAllLoans()
        {
            return await _context.Loan.ToListAsync();
        }
        public async Task<Loan> SelectLoanById(int loanId)
        {
            return await _context.Loan
                .FirstOrDefaultAsync(loan => loan.Id == loanId);
        }
        public async Task<Loan> InsertNewLoan(Loan loan)
        {
            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }
        public async Task<Loan> UpdateExistingLoan(int loanId, Loan loan)
        {
            Loan updateLoan = await _context.Loan.FirstOrDefaultAsync(loan=>loan.Id == loanId);
            if (updateLoan != null)
            {
                updateLoan.userID = loan.userID;
                updateLoan.bookId = loan.bookId;
                updateLoan.loaned_At = loan.loaned_At;
                updateLoan.return_date = loan.return_date;

                await _context.SaveChangesAsync();
            }
            return updateLoan;

        }
        public async Task<Loan> DeleteLoanById(int loanId)
        {
            Loan deleteLoan = await _context.Loan.FirstOrDefaultAsync(loan => loan.Id == loanId);
            if (deleteLoan != null)
            {
               
                _context.Loan.Remove(deleteLoan);
                await _context.SaveChangesAsync();
            }
            return deleteLoan;

        }
    }
}
