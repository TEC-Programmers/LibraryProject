using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTO;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
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

        public async Task<LoanResponse> CreateLoan(LoanRequest newLoan)
        {
            Loan Loan = MapLoanRequestToLoan(newLoan);

            Loan insertedLoan = await _loanRepository.InsertNewLoan(Loan);

            if (insertedLoan != null)
            {
                return MapLoanToLoanResponse(insertedLoan);
            }
            return null;
        }

        public async Task<LoanResponse> DeleteLoan(int LoanId)
        {
            Loan deletedLoan = await _loanRepository.DeleteLoanById(LoanId);

            if (deletedLoan != null)
            {
                return MapLoanToLoanResponse(deletedLoan);
            }
            return null;
        }

        public async Task<List<LoanResponse>> GetAllLoans()
        {
            List<Loan> Loans = await _loanRepository.SelectAllLoans();
            return Loans.Select(Loan => MapLoanToLoanResponse(Loan)).ToList();
        }

        public async Task<LoanResponse> GetLoanById(int LoanId)
        {
            Loan Loan = await _loanRepository.SelectLoanById(LoanId);
            if (Loan != null)
            {
                return MapLoanToLoanResponse(Loan);
            }
            return null;
        }

        public async Task<LoanResponse> UpdateLoan(int LoanId, LoanRequest updateLoan)
        {
            Loan Loan = MapLoanRequestToLoan(updateLoan);

            Loan updatedLoan = await _loanRepository.UpdateExistingLoan(LoanId, Loan);

            if (updatedLoan != null)
            {
                return MapLoanToLoanResponse(updatedLoan);
            }
            return null;
        }

        private Loan MapLoanRequestToLoan(LoanRequest loanRequest)
        {
            return new Loan()
            {
                userID = loanRequest.userID,
                bookId = loanRequest.bookId,
                loaned_At = loanRequest.loaned_At,
                return_date = loanRequest.return_date,
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
