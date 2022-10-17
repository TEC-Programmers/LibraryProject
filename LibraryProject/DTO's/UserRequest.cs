using System.ComponentModel.DataAnnotations;

namespace LibraryProject.API.DTO_s
{
    public class UserRequest
    {
        [Required]
        [StringLength(255, ErrorMessage = "Email must be less than 128 chars")]
        public string Email { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Password must be less than 255 chars")]
        public string Password { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "FirstName must be less than 32 chars")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "MiddleName must be less than 32 chars")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "LastName must be less than 32 chars")]
        public string LastName { get; set; }
    }
}
