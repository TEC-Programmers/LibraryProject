using LibraryProject.API.Database.Entities;
using LibraryProject.API.Helpers;
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
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Loan> Loan { get; set; }


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
            modelBuilder.Entity<Publisher>().HasData(
               new()
               {
                   Id = 1,
                   Name = "Gyldendal",                 
               },
               new()
               {
                   Id = 2,
                   Name = "Rosinante",
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
                    Title = "Pippi Langstrømper",
                    Description = "BØg for børn",
                    Language ="Danish",
                    PublishYear=1945,
                    CategoryId = 1,
                    AuthorId = 1,
                    PublisherId = 1,
                },
                new()
                {
                    Id = 2,
                    Title = "Rødby-Puttgarden",
                    Description = "Romaner for voksen2",
                    Language = "Danish",
                    PublishYear = 2005,
                    CategoryId = 2,
                    AuthorId = 2,
                    PublisherId = 2,
                }
                );
            modelBuilder.Entity<User>().HasData(
                new()
                {
                    Id = 1,
                    FirstName = "Peter",
                    MiddleName = "Per.",
                    LastName = "Aksten",
                    Email = "peter@abc.com",
                    Password = "password",
                    Role = Role.Administrator
                },
                new()
                {
                    Id = 2,
                    FirstName = "Rizwanah",
                    MiddleName = "R.R",
                    LastName = "Mustafa",
                    Email = "riz@abc.com",
                    Password = "password",
                    Role = Role.Customer
                }
                );
            modelBuilder.Entity<Loan>().HasData(
                new()
                {
                    Id = 1,
                    userID = 2,
                    bookId = 2,
                    loaned_At = "06/05/22",
                    return_date = "13/05/22"
                },
                new()
                {
                    Id = 3,
                    userID = 4,
                    bookId = 5,
                    loaned_At = "27/06/22",
                    return_date = "27/07/22"
                }
                );
            modelBuilder.Entity<Reservation>().HasData(
                new()
                {
                    reservationId = 1,
                    userId = 1,
                    bookId = 1,
                    reserved_At = "06/05/22",
                    reserved_To = "13/05/22"
                },
                new()
                {
                    reservationId = 2,
                    userId = 2,
                    bookId = 2,
                    reserved_At = "14/05/22",
                    reserved_To = "21/05/22"
                }
                );

        }

    }
}
