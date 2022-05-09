using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Database
{
    public class LibraryProjectContext :DbContext
    {
        public LibraryProjectContext() { }
        public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options) : base(options) { }
    }
}
