using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryProject.API.Helpers;

namespace LibraryProject.Database.Entities
{
    //Creating User Entity for the database
    public class User
    {
        [Key]   //[Key]fortæller EntityFramworkat Idegenskaben skal være den primære nøgle.
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
