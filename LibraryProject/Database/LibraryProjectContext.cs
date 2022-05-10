using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Database
{
    public class LibraryProjectContext : DbContext
    {
        public LibraryProjectContext() { }
        public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options) : base(options) { }


        public DbSet<Reservation> reservation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().HasData(
                new()
                {
                    Id = 1,
                    userId = 1,
                    bookId = 1,
                    reserved_At = "06/05/22",
                    reserved_To = "13/05/22"

                },
                new()
                {
                    Id = 2,
                    userId = 2,
                    bookId = 2,
                    reserved_At = "14/05/22",
                    reserved_To = "21/05/22"
                }
             );
        }

    }
}
