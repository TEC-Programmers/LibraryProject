using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.Database.Entities
{
    //Creating Author Entity for the database
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string MiddleName { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string LastName { get; set; }

        //public int bookId { get; set; }

        //[ForeignKey("bookId")]
        //public Book Book { get; set; }
    }
}
