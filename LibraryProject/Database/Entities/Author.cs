using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.Database.Entities
{
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

     
    }
}
