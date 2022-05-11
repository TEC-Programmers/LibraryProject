using LibraryProject.Controllers;
using LibraryProject.DTO_s;
using LibraryProject.Services;
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
                loaned_At = "7/7/2022",
                return_date = "7/8/2022"
                
             });

            loans.Add(new()
            {
                Id = 2,
                userID = 2,
                bookId = 2,
                loaned_At = "16/7/2022",
                return_date = "16/8/2022"

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


    }
    
    
}
