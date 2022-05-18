using LibraryProject.API.DTO;
using LibraryProject.API.Services;
using LibraryProject.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Controllers
{

    public class LoanControllerTests
    {

        private readonly LoanController _loanController;
        private readonly Mock<ILoanService> _mockLoanService = new();

        public LoanControllerTests()
        {
            _loanController = new(_mockLoanService.Object);
        }

        [Fact]
        public async void GetAll_shouldReturnStatusCode200_WhenLoansExists()
        {
            //Arrange
            List<LoanResponse> loans = new();

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
                return_date = "24/7/2022"

            });
            _mockLoanService
                .Setup(x => x.GetAllLoans())
                .ReturnsAsync(loans);


            //Act
            var result = await _loanController.GetAll();
            ////Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void GetAll_shouldReturnStatusCode204_WhenNoLoansExists()
        {
            //Arrange
            List<LoanResponse> loans = new();

            //loans.Add(new());
            //loans.Add(new());
            _mockLoanService
                .Setup(x => x.GetAllLoans())
                .ReturnsAsync(loans);

            //Act
            var result = await _loanController.GetAll();
            ////Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_shouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //Arrange
            _mockLoanService
                .Setup(x => x.GetAllLoans())
                .ReturnsAsync(() => null);


            //Act
            var result = await _loanController.GetAll();
            ////Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_shouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            _mockLoanService
                .Setup(x => x.GetAllLoans())
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));


            //Act
            var result = await _loanController.GetAll();
            ////Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange
            int loanId = 1;

            LoanResponse loan = new()
            {
                Id = loanId,
                userID = 1,
                bookId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
            };

            _mockLoanService
                .Setup(x => x.GetLoanById(It.IsAny<int>()))
                .ReturnsAsync(loan);

            //Act
            var result = await _loanController.GetById(loanId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async Task GetById_ShouldReturnStatusCode404_WhenLoanDoesNotExist()
        {
            //Arrange
            int loanId = 1;

            _mockLoanService
                .Setup(x => x.GetLoanById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _loanController.GetById(loanId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            _mockLoanService
                .Setup(x => x.GetLoanById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _loanController.GetById(1);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task Create_ShouldReturnStatusCode200_WHenLoanIsSuccessfullyCreated()
        {
            //Arrange
            LoanRequest newLoan = new()
            {
               userID = 1,
               bookId = 1,
               loaned_At = "11/5/2022",
               return_date = "11/6/2022"
               
            };

            int loanId = 1;

            LoanResponse loanResponse = new()
            {
                Id = loanId,
                userID = 1,
                bookId=1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
                
            };

            _mockLoanService
                .Setup(x => x.CreateLoan(It.IsAny<LoanRequest>()))
                .ReturnsAsync(loanResponse);

            //Act
            var result = await _loanController.Create(newLoan);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            LoanRequest newLoan = new()
            {
               userID = 1,
               bookId = 1,
               loaned_At= "11/5/2022",
               return_date = "11/6/2022"
            };

            _mockLoanService
                .Setup(x => x.CreateLoan(It.IsAny<LoanRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _loanController.Create(newLoan);


            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenLoanIsSuccessfullyUpdated()
        {
            //Arrange
            LoanRequest updateloan = new()
            {
                userID= 1,
                bookId= 1,
                loaned_At ="11/5/2022",
                return_date = "11/6/2022"
            };

            int loanId = 1;

            LoanResponse loanResponse = new()
            {
                Id = loanId,
                userID = 1,
                bookId = 1,
                loaned_At = "11/5/2022",
                return_date= "11/6/2022"
            };

            _mockLoanService
                .Setup(x => x.UpdateLoan(It.IsAny<int>(), It.IsAny<LoanRequest>()))
                .ReturnsAsync(loanResponse);

            //Act
            var result = await _loanController.Update(loanId, updateloan);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenTryingToUpdateLoanWhichDoesNotExist()
        {
            //Arrange
            LoanRequest updateloan = new()
            {
                userID = 1,
                bookId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
            };

            int loanId = 1;

            _mockLoanService
                .Setup(x => x.UpdateLoan(It.IsAny<int>(), It.IsAny<LoanRequest>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _loanController.Update(loanId, updateloan);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            LoanRequest updateloan = new()
            {
                userID=1,
                bookId=1,
                loaned_At = "11/5/2022",
                return_date= "11/6/2022"
            };

            int loanId = 1;

            _mockLoanService
                .Setup(x => x.UpdateLoan(It.IsAny<int>(), It.IsAny<LoanRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _loanController.Update(loanId, updateloan);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenLoanIsDeleted()
        {
            //Arrange
            int loanId = 1;

            LoanResponse loanResponse = new()
            {
                Id = loanId,
                userID=1,
                bookId = 1,
                loaned_At= "11/5/2022",
                return_date = "11/5/2022"
            };

            _mockLoanService
                .Setup(x => x.DeleteLoan(It.IsAny<int>()))
                .ReturnsAsync(loanResponse);

            //Act
            var result = await _loanController.Delete(loanId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTryingToDeleteLoanWhichDoesNotExist()


        {
            //Arrange
            int loanId = 1;

            _mockLoanService
                .Setup(x => x.DeleteLoan(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _loanController.Delete(loanId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int loanId = 1;

            _mockLoanService
                .Setup(x => x.DeleteLoan(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _loanController.Delete(loanId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


    }
    
    
}
