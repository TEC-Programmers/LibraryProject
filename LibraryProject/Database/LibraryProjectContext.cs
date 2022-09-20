
﻿using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryProject.API.Database.Entities;
using LibraryProject.API.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Data;
using BC = BCrypt.Net.BCrypt;


namespace LibraryProject
{
    public class LibraryProjectContext : DbContext
    {
        public LibraryProjectContext() { }
        public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options) : base(options) { }  //creating instances of the class 
        public DbSet<Book> Book { get; set; }   //represents the set of book enitity in EFC(Entity Framework Core)
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Loan> Loan { get; set; }
              
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //We override the method to configure the models of the set of entities
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
                    Description = "Bog for børn",
                    Language = "Danish",
                    Image="Book1.png",
                    PublishYear = 1945,
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
                    Image = "Book2.png",
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
                    Password = BC.HashPassword("password"),
                    Role = Role.Administrator
                },
                new()
                {
                    Id = 2,
                    FirstName = "Rizwanah",
                    MiddleName = "R.R",
                    LastName = "Mustafa",
                    Email = "riz@abc.com",
                    Password = BC.HashPassword("password"),
                    Role = Role.Customer
                }
                );
            modelBuilder.Entity<Loan>().HasData(
                new()
                {
                    Id = 1,
                    userId = 2,
                    bookId = 2,
                    loaned_At = "2022/05/07",
                    return_date = "2022/05/08"
                },
                new()
                {
                    Id = 3,
                    userId = 4,
                    bookId = 5,
                    loaned_At = "2022/06/07",
                    return_date = "2022/07/08",
                }
                );
            modelBuilder.Entity<Reservation>().HasData(
                new()
                {
                    Id = 1,
                    userId = 2,
                    bookId = 2,
                    reserved_At = "2022/08/08",
                    reserved_To = "2022/09/09"
                },
                new()
                {
                    Id = 2,
                    userId = 2,
                    bookId = 5,
                    reserved_At = "2022/08/08",
                    reserved_To = "2022/08/09"
                }
                );
        }
    }
}
