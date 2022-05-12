using LibraryProject.Database.Entities;
using LibraryProject.DTO_s;
using LibraryProject.Repositories;
using LibraryProject.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Services
{
    public class loanServiceServicTests
    {
        private readonly LoanService _loanService;
        private readonly Mock<ILoanRepository> _mockloanServiceRepository = new();
        public loanServiceServicTests()
        {
            _loanService = new LoanService(_mockloanServiceRepository.Object);
        }

        [Fact]
        public async void GetAllloanServices_ShouldReturnListOfloanResponses_WhenLoansExists()
        {
            //Arrange 
            List<Loan> loans = new();

            loans.Add(new()
            {
                Id = 1,
                userID = 1,
                bookId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
            });

            loans.Add(new()
            {
                Id = 2,
                userID = 2,
                bookId = 2,
                loaned_At = "24/6/2022",
                return_date= "24/7/2022"
            });

            _mockloanServiceRepository
             .Setup(x => x.SelectAllLoans())
             .ReturnsAsync(loans);

            //Act 
            var result = await _loanService.GetAllLoans();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<LoanResponse>>(result);
        }

        [Fact]
        public async void GetAllLoans_ShouldReturnListOfEmptyLoanResponses_WhenNoLoansExists()
        {
            //Arrange
            List<Loan> loans = new();

            _mockloanServiceRepository
                .Setup(x => x.SelectAllLoans())
                .ReturnsAsync(loans);

            //Act
            var result = await _loanService.GetAllLoans();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<LoanResponse>>(result);



        }
        [Fact]
        public async void GetLoanById_ShouldReturnLoanResponse_WhenLoanExists()
        {
            // Arrange 
            int loanId = 1;

            Loan loan = new()
            {
                Id = 1,
                userID = 1,
                bookId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
            };

            _mockloanServiceRepository
                .Setup(x => x.SelectLoanById(It.IsAny<int>()))
                .ReturnsAsync(loan);

            // Act
            var result = await _loanService.GetLoanById(loanId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LoanResponse>(result);
            Assert.Equal(loan.Id, result.Id);
            Assert.Equal(loan.userID, result.userID);
            Assert.Equal(loan.bookId, result.bookId);
            Assert.Equal(loan.loaned_At, result.loaned_At);
            Assert.Equal(loan.return_date, result.return_date);
        }
        [Fact]
        public async void GetloanById_ShouldReturnNull_WhenLoanDoesNotExist()
        {
            //Arrange
            int loanId = 1;

            _mockloanServiceRepository
                .Setup(x => x.SelectLoanById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _loanService.GetLoanById(loanId);

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async void CreateLoan_ShouldReturnLoanResponse_WhenCreateIsSuccess()
        {
            //Arrange
            LoanRequest newloan = new()
            {
                userID = 1,
                bookId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
                
            };

            int loanId = 1;

            Loan createloan = new()
            {
                Id = loanId,
                userID = 1,
                bookId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
                
            };

            _mockloanServiceRepository
                .Setup(x => x.InsertNewLoan(It.IsAny<Loan>()))
                .ReturnsAsync(createloan);

            //Act
            var result = await _loanService.CreateLoan(newloan);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<LoanResponse>(result);
            Assert.Equal(loanId, result.Id);
            Assert.Equal(newloan.userID, result.userID);
            Assert.Equal(newloan.bookId, result.bookId);
            Assert.Equal(newloan.loaned_At, result.loaned_At);
            Assert.Equal(newloan.return_date, result.return_date);
        }
        [Fact]
        public async void CreateLoan_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            // Arrange 
            LoanRequest newloan = new()
            {

            };

            _mockloanServiceRepository
                .Setup(x => x.InsertNewLoan(It.IsAny<Loan>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _loanService.CreateLoan(newloan);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateLoan_ShouldReturnLoanResponse_WhenUpdateIsSuccess()
        {
            LoanRequest loan = new()
            {
               userID = 1,
               bookId = 1,
               loaned_At = "11/5/2022",
               return_date = "11/6/2022"
               

            };

        }



        [Fact]
        public async void UpdateloanService_ShouldReturnNull_WhenloanServiceDoesNotExist()
        {
            // Arrange 
            LoanRequest loanRequest = new()
            {
               i
            };

            int loanServiceId = 1;

            _mockloanServiceRepository
                .Setup(x => x.UpdateExistingloanService(It.IsAny<int>(), It.IsAny<loanService>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _loanService.UpdateloanService(loanServiceId, LoanRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateloanService_ShouldReturnloanServiceResponse_WhenUpdateIsSuccess()
        {
            //Arrange
            LoanRequest LoanRequest = new()
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R",
                BirthYear = 1948
            };
            int loanServiceId = 1;

            LoanService loanService = new()
            {
                Id = loanServiceId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R",
                BirthYear = 1948
            };

            _mockloanServiceRepository
                 .Setup(x => x.UpdateExistingloanService(It.IsAny<int>(), It.IsAny<LoanService>()))
                 .ReturnsAsync(loanService);

            //Act 
            var result = await _loanService.UpdateloanService(loanServiceId, LoanRequest);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<loanServiceResponse>(result);
            Assert.Equal(loanServiceId, result.Id);
            Assert.Equal(LoanRequest.FirstName, result.FirstName);
            Assert.Equal(LoanRequest.LastName, result.LastName);
            Assert.Equal(LoanRequest.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void DeleteloanService_ShouldReturnloanServiceResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int loanServiceId = 1;

            LoanService deletedloanService = new()
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R",
                BirthYear = 1948
            };

            _mockloanServiceRepository
                .Setup(x => x.DeleteloanService(It.IsAny<int>()))
                .ReturnsAsync(deletedloanService);

            // Act
            var result = await _loanService.DeleteloanService(loanServiceId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<loanServiceResponse>(result);
            Assert.Equal(loanServiceId, result.Id);
        }
        [Fact]
        public async void DeleteloanService_ShouldReturnNull_WhenloanServiceDoesNotExist()
        {
            // Arrange
            int loanServiceId = 1;

            _mockloanServiceRepository
                .Setup(x => x.DeleteloanService(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _loanService.DeleteloanService(loanServiceId);

            // Assert
            Assert.Null(result);
        }
    }
}
