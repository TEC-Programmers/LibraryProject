using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTO;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using LibraryProject.API.Services;
using LibraryProject.Database.Entities;
using LibraryProject.DTO_s;
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
                userID = 1,
                bookId=1,
                loaned_At = "11/5/2022",
                return_date= "11/6/2022"
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
            LoanRequest loanRequest = new()
            {
               userID = 1,
               bookId = 1,
               loaned_At = "11/5/2022",
               return_date = "11/6/2022"
               

            };

            int loanId = 1;

            Loan loan = new()
            {
                Id = loanId,
                userID= 1,
                bookId= 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022" 
            };


        
          _mockloanServiceRepository.Setup(x => x.UpdateExistingLoan(It.IsAny<int>(), It.IsAny<Loan>()))
                .ReturnsAsync(loan);

        //Act
        var result = await _loanService.UpdateLoan(loanId, loanRequest);


        //Assert
        Assert.NotNull(result);
            Assert.IsType<LoanResponse>(result);
            Assert.Equal(loanId, result.Id);
            Assert.Equal(loanRequest.userID, result.userID);
            Assert.Equal(loanRequest.bookId, result.bookId);
            Assert.Equal(loanRequest.loaned_At, result.return_date);
            Assert.Equal(loanRequest.loaned_At, result.return_date);
        }

        [Fact]
        public async void UpdateLoan_ShouldReturnNull_WhenLoanDoesNotExist()
        {
            // Arrange 
            LoanRequest loanRequest = new()
            {
               userID=1,
               bookId=1,
               loaned_At= "11/5/2022",
                return_date = "11/6/2022"
            };

            int loanId = 1;

            _mockloanServiceRepository
                .Setup(x => x.UpdateExistingLoan(It.IsAny<int>(), It.IsAny<Loan>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _loanService.UpdateLoan(loanId, loanRequest);

            // Assert
            Assert.Null(result);
        }

       
        [Fact]
        public async void DeleteLoan_ShouldReturnLoanResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int loanId = 1;

            Loan deletedloan = new()
            {
                Id= 1,
                userID= 1,
                bookId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
            };

            _mockloanServiceRepository
                .Setup(x => x.DeleteLoanById(It.IsAny<int>()))
                .ReturnsAsync(deletedloan);

            // Act
            var result = await _loanService.DeleteLoan(loanId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LoanResponse>(result);
            Assert.Equal(loanId, result.Id);
        }
        [Fact]
        public async void DeleteLoan_ShouldReturnNull_WhenLoanDoesNotExist()
        {
            // Arrange
            int loanId = 1;

            _mockloanServiceRepository
                .Setup(x => x.DeleteLoanById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _loanService.DeleteLoan(loanId);

            // Assert
            Assert.Null(result);
        }
    }
}
