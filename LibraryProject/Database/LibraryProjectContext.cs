using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Database
{
    public class LibraryProjectContext : DbContext
    {
        public LibraryProjectContext() { }
        public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options) : base(options) { }

        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasData(
              new()
              {
                  Id = 1,
                  CategoryName = "KidsBook"


              },
               new()
               {
                   Id = 2,
                   CategoryName = "Roman"
               }
              );
            modelBuilder.Entity<Book>().HasData(
                new()
                {
                    Id = 1,
                    Title = " Pippi Langstrømper",
                    Description = "BØg for børn",
                    Language ="Danish",
                    PublishYear=1945,
                    CategoryId = 1

                },
                new()
                {
                    Id = 2,
                    Title = "Rødby-Puttgarden",
                    Description = "Romaner for voksen2",
                    Language = "Danish",
                    PublishYear = 2005,
                    CategoryId = 2
                }
                );
        }

    }
}
