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

        public Task<Loan> DeleteLoanById(int loanId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Loan> InsertNewLoan(Loan loan)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Loan>> SelectAllLoans()
        {
            return await _context.loan.ToListAsync();
        }
        public async Task<Loan> SelectLoanById(string loanId)
        {
            return null;
        }

        public Task<Loan> SelectLoanById(int loanId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Loan> UpdateExistingLoan(int loanId, Loan loan)
        {
            throw new System.NotImplementedException();
        }
    }
}
