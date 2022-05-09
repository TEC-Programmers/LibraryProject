using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Database
{
    public class LibraryProjectContext :DbContext
    {
        public LibraryProjectContext() { }
        public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options) : base(options) { }

        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
