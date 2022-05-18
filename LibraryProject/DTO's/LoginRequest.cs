using System.ComponentModel.DataAnnotations;

namespace LibraryProject.API.DTO_s
{
    public class LoginRequest
    {
        [Required]
        [StringLength(128, ErrorMessage = "Email must be less than 128 chars")]
        public string Email { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Password must be less than 32 chars")]
        public string Password { get; set; }
    }
}
