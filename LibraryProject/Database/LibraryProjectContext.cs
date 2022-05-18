

using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Database
{
    public class LibraryProjectContext: DbContext
    {
        public LibraryProjectContext() { }
        public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options) : base(options) { }
        public DbSet<Loan> loan { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>().HasData(
            new()
            {
                Id = 1,
                userID = 1,
                bookId = 1,
                loaned_At = "06/05/22",
                return_date = "13/05/22"
            },
            new()
            {
                Id = 2,
                userID = 2,
                bookId = 2,
                loaned_At = "27/06/22",
                return_date = "27/07/22"
            }
            );
        }

    }

  
}
