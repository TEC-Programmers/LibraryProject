using LibraryProject.Database.Entities;
using LibraryProject.DTO_s;
using LibraryProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryProject.Services
{
    public interface ILoanService
    {
        List<LoanResponse> GetAllLoans();
    }

    public class LoanService : ILoanService
    {
        private readonly ILoanService _loanService;
        private readonly ILoanRepository _loanRepository;
        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public LoanService(ILoanService loanService)
        {
            _loanService = loanService;
        }
        public List<LoanResponse> GetAllLoans()
        {
            List<LoanResponse> loans = new();

            loans.Add(new()
            {
                Id = 1,
                userID = 1,
                bookId = 1,
                loaned_At = "10/05/22",
                return_date = "10/06/22"
            });
            return loans;
        }
    }
}
