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
   
    }
}
