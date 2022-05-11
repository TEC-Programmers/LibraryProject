
using LibraryProject.Database.Entities;
using LibraryProject.DTO_s;
using LibraryProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.Services
{
    public interface ILoanService
    {
       Task<List<LoanResponse>> GetAllLoans();
    }

    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public  async Task<List<LoanResponse>> GetAllLoans()
        {
            List<Loan> loans =  await _loanRepository.SelectAllLoans();

            return loans.Select(loan => new LoanResponse
            {
                Id = loan.Id,
                userID = loan.userID,
                bookId = loan.bookId,
                loaned_At = loan.loaned_At,
                return_date = loan.return_date,
            }).ToList();
            
        }
    }
}
