
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
       Task<LoanResponse> GetLoanById(int loanId);
       Task<LoanResponse> CreateLoan(LoanRequest newLoan);
       Task<LoanResponse> UpdateLoan(int loanId, LoanRequest updateloan);
        Task<LoanResponse> DeleteLoan(int loanId);
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
        public async Task<LoanResponse> GetLoanById(int loanId)
        {
            Loan loan = await _loanRepository.SelectLoanById(loanId);

            if (loan != null)
            {
                return MapLoanToLoanResponse(loan);

            }
            return null;
        }

        public async Task<LoanResponse> CreateLoan(LoanRequest newLoan)
        {
            Loan loan = MapLoanRequestToLoan(newLoan);

            Loan insertedLoan = await _loanRepository.InsertNewLoan(loan);

            if (insertedLoan != null)
            {
                return MapLoanToLoanResponse(insertedLoan);
            }
            return null;

        }

        public async Task<LoanResponse> UpdateLoan(int loanId, LoanRequest updateLoan)
        {
            Loan loan = new()
            {
                userID = updateLoan.userID,
                bookId = updateLoan.bookId,
                loaned_At = updateLoan.loaned_At,
                return_date = updateLoan.return_date,
            };

            Loan updatedLoan = await _loanRepository.UpdateExistingLoan(loanId,loan);
            if (updatedLoan != null)
            {
                return new LoanResponse()
                {
                    Id = updatedLoan.Id,
                    userID = updatedLoan.userID,
                    bookId = updatedLoan.bookId,
                    loaned_At = updatedLoan.loaned_At,
                    return_date = updatedLoan.return_date,

                };
            }
            return null;

        }

        public async Task<LoanResponse> DeleteLoan(int loanId)
        {
            Loan deletedloan = await _loanRepository.DeleteLoanById(loanId);
            if (deletedloan != null)
            {
                return new LoanResponse
                {
                    Id = deletedloan.Id,
                    userID = deletedloan.userID,
                    bookId = deletedloan.bookId,
                    loaned_At = deletedloan.loaned_At,
                    return_date = deletedloan.return_date,
                };
            }
        
              
            return null;

        }

        private Loan MapLoanRequestToLoan(LoanRequest loanRequest)
        {
            return new Loan()
            {
               userID = loanRequest.userID,
               bookId= loanRequest.bookId,
               loaned_At= loanRequest.loaned_At,
               return_date= loanRequest.return_date,

            };
        }
        private LoanResponse MapLoanToLoanResponse(Loan loan)
        {
            return new LoanResponse()
            {
                Id = loan.Id,
                userID = loan.userID,
                bookId = loan.bookId,
                loaned_At = loan.loaned_At,
                return_date = loan.return_date,
            };
        }


    }
}
