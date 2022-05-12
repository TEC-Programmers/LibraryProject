using LibraryProject.API.Helpers;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Database.Entities;

namespace LibraryProject
{
    public class LibraryProjectContext : DbContext
    {
        public LibraryProjectContext() { }
        public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options) : base(options) { }

        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }

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
                    Language = "Danish",
                    PublishYear = 1945,
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

        } 
        

       

   
    }
}
