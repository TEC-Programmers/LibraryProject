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
        public DbSet<Author> Author { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasData(
              new()
              {
                  Id = 1,
                  CategoryName = "Børnebog"


              },
               new()
               {
                   Id = 2,
                   CategoryName = "Roman"
               }
              );
            modelBuilder.Entity<Author>().HasData(
              new()
              {
                  Id = 1,
                  FirstName = "Astrid",
                  MiddleName = "",
                  LastName = " Lindgrens"


              },
               new()
               {
                   Id = 2,
                   FirstName = "Helle",
                   MiddleName = "",
                   LastName = "Helle"
               }
              );
            modelBuilder.Entity<Book>().HasData(
                new()
                {
                    Id = 1,
                    Title = " Pippi Langstrømper",
                    Description = "BØg for børn",
                    Language ="Dansk",
                    PublishYear=1945,
                    CategoryId = 1,
                    AuthorId = 1

                },
                new()
                {
                    Id = 2,
                    Title = "Rødby-Puttgarden",
                    Description = "Romaner for voksen2",
                    Language = "Danish",
                    PublishYear = 2005,
                    CategoryId = 2,
                    AuthorId = 2
                }
                );
            
        }

    }
}
