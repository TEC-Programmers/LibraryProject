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
            return await _context.loan.ToListAsync();
        }
        public async Task<Loan> SelectLoanById(int loanId)
        {
            return await _context.loan
                .FirstOrDefaultAsync(loan => loan.Id == loanId);
        }
        public async Task<Loan> InsertNewLoan(Loan loan)
        {
            _context.loan.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }
        public async Task<Loan> UpdateExistingLoan(int loanId, Loan loan)
        {
            Loan updateLoan = await _context.loan.FirstOrDefaultAsync(loan=>loan.Id == loanId);
            if (updateLoan != null)
            {
                updateLoan.Id = loan.Id;
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
            Loan deleteLoan = await _context.loan.FirstOrDefaultAsync(loan => loan.Id == loanId);
            if (deleteLoan != null)
            {
               
                _context.loan.Remove(deleteLoan);
                await _context.SaveChangesAsync();
            }
            return deleteLoan;

        }
    }
}
