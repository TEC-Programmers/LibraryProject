using Microsoft.EntityFrameworkCore;
using LibraryProject.API.Helpers;
using LibraryProject.Database.Entities;

namespace LibraryProject.Database
{
    public class LibraryProjectContext :DbContext
    {
        public LibraryProjectContext() { }
        public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options) : base(options) { }

  
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<User>().HasData(
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
