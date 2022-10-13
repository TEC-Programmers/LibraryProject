using LibraryProject.API.Database.Entities;
using LibraryProject.API.Repositories;
using LibraryProject.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Repositories
{
    public class LoanRepositoryTests
    {
        private readonly DbContextOptions<LibraryProjectContext> _options;
        private readonly LibraryProjectContext _context;
        private readonly LoanRepository _loanRepository;
        private readonly IConfiguration _configuration;

        public LoanRepositoryTests(IConfiguration configuration)
        {
            _configuration = configuration;
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
            .UseInMemoryDatabase(databaseName: "LibraryProjectLoans")
            .Options;

            _context = new(_options);

            _loanRepository = new(_context,_configuration);
        }

        [Fact]
        public async void SelectAllLoans_ShouldReturnListOfLoans_WhenLoansExists()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Loan.Add(new()
            {
                Id = 1,
                userId = 1,
                bookId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"

            });
            _context.Loan.Add(new()
            {
                Id = 2,
                userId = 2,
                bookId = 2,
                loaned_At = "24/6/2022",
                return_date = "24/7/2022"

            });

            await _context.SaveChangesAsync();
        }
        [Fact]
        public async void SelectAllLoans_ShouldReturnEmptyListOfLoans_WhenNoLoansExists()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();

            //act
            var result = await _loanRepository.SelectAllLoansWithProcedure();

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Loan>>(result);
            Assert.Empty(result);

        }
        [Fact]
        public async void SelectLoanById_ShouldReturnLoan_WhenLoanExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int LoanId = 1;

            _context.Loan.Add(new()
            {
                Id = LoanId,
                userId = 1,
                bookId = 1,
                loaned_At = "11/5/22",
                return_date = "11/6/22"
            });
            await _context.SaveChangesAsync();

            //Act
            var result = await _loanRepository.SelectLoanById(LoanId);

            //Assert
            Assert.NotNull(_context.Loan);
            Assert.IsType<Loan>(result);
            Assert.Equal(LoanId, result.Id);
        }

        [Fact]
        public async void SelectLoanById_ShouldReturnNull_WhenLoanDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _loanRepository.SelectLoanById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewLoan_ShouldAddnewIdToLoan_WhenSavingToDatabase()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int expectedNewId = 1;

            Loan loan = new()
            {
                bookId = 1,
                userId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
            };

            //Act
            var result = await _loanRepository.InsertNewLoanWithProcedure(loan);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Loan>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void InsertNewLoan_ShouldFailToAddNewLoan_WhenLoanIdAlreadyExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
          
            Loan Loan = new()
            {
                Id = 1,
                bookId = 1,
                userId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
            };

            _context.Loan.Add(Loan);
            await _context.SaveChangesAsync();

            //Act
            async Task action() => await _loanRepository.InsertNewLoanWithProcedure(Loan);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added.", ex.Message);


        }

        [Fact]
        public async void UpdateExistingLoan_ShouldChangeValuesOnLoan_WhenLoanExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();

            int LoanId = 1;

            Loan newLoan = new()
            {
                Id = LoanId,
                bookId = 1,
                userId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
            };

            _context.Loan.Add(newLoan);
            await _context.SaveChangesAsync();

            Loan updateLoan = new()
            {
                Id = LoanId,
                bookId = 1,
                userId = 1,
                loaned_At = "updated 11/5/2022",
                return_date = " updated 11/6/2022"
            };

            //Act
            var result = await _loanRepository.UpdateExistingLoan(LoanId, updateLoan);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Loan>(result);
            Assert.Equal(LoanId, result.Id);
            Assert.Equal(updateLoan.bookId, result.bookId);
            Assert.Equal(updateLoan.userId, result.userId);
            Assert.Equal(updateLoan.loaned_At, result.loaned_At);
            Assert.Equal(updateLoan.return_date, result.return_date);

        }
        [Fact]
        public async void UpdateExistingLoan_ShouldReturnNull_WhenLoanDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int LoanId = 1;

            Loan updateLoan = new()
            {
                Id = LoanId,
                bookId = 1,
                userId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022",
            };

            // Act
            var result = await _loanRepository.UpdateExistingLoan(LoanId, updateLoan);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteLoanById_ShouldReturnDeletedLoan_WhenLoanIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int LoanId = 1;

            Loan newLoan = new()
            {
                Id = LoanId,
                bookId = 1,
                userId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
            };

            _context.Loan.Add(newLoan);
            await _context.SaveChangesAsync();

            // Act
            var result = await _loanRepository.DeleteLoanByIdWithProcedure(LoanId);
            var Loan = await _loanRepository.SelectLoanById(LoanId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Loan>(result);
            Assert.Equal(LoanId, result.Id);
            Assert.Null(Loan);
        }
        [Fact]
        public async void DeleteLoanById_ShouldReturnNull_WhenLoanDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _loanRepository.DeleteLoanByIdWithProcedure(1);

            // Assert
            Assert.Null(result);
        }



    }
}
