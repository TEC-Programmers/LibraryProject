using LibraryProject.Database;
using LibraryProject.Database.Entities;
using LibraryProject.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public LoanRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
            .UseInMemoryDatabase(databaseName: "LibraryProjectLoans")
            .Options;

            _context = new(_options);

            _loanRepository = new(_context);
        }

        [Fact]
        public async void SelectAllLoans_ShouldReturnListOfLoans_WhenLoansExists()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();
            _context.loan.Add(new()
            {
                Id = 1,
                userID = 1,
                bookId = 1,
                loaned_At = "11/5/2022",
                return_date = "11/6/2022"
                
            });
            _context.loan.Add(new()
            {
                Id= 2,
                userID = 2,
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
            var result = await _loanRepository.SelectAllLoans();

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Loan>>(result);
            Assert.Empty(result);
            
        }



    }
}
