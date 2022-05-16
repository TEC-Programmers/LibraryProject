using LibraryProject.API.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.API.Database.Entities
{
    public class User
    {
        [Key]   //[Key]fortæller EntityFrameworket Idegenskaben skal være den primære nøgle.
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string MiddleName { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Password { get; set; }


        // Role er en enum datatype, der består af integrerede konstanter. Her bruges vi enum for at sætter role(Admin eller Kunder)
        public Role Role { get; set; }
    }
}
