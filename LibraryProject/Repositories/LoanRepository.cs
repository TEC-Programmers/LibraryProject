using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LibraryProject.Repositories
{

    public interface ILoanRepository
    {
        List<Loan> SelectAllLoans();
    }
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryProjectContext _context;
        public LoanRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public List<Loan> SelectAllLoans()
        {
            return _context.loan.ToList();
        }
    }
}
