using LibraryProject.API.Database.Entities;
using LibraryProject.Database;
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
    public class LoanRepository : ILoanRepository     // This class is inheriting interfcae ILoanRepository and implement the interfaces
    {
        private readonly LibraryProjectContext _context;        //making an instance of the class LibraryProjectContext
        public LoanRepository(LibraryProjectContext context)      //dependency injection with parameter 
        {
            _context = context;
        }

        //**implementing the methods of IAuthorRepository interface**// 

        //this method will get details of all Loans
        public async Task<List<Loan>> SelectAllLoans()
        {
            return await _context.Loan.ToListAsync();
        }

        //this method will get info of one bookloan by specific ID
        public async Task<Loan> SelectLoanById(int loanId)
        {
            return await _context.Loan
                .FirstOrDefaultAsync(loan => loan.Id == loanId);
        }

        //This method will add a new Loan to the system
        public async Task<Loan> InsertNewLoan(Loan loan)
        {
            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }
        //This method will update the information of the specific loan by ID
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

        }   //This method will remove one specific Author whose Id has been got
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
