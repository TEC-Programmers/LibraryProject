using System.ComponentModel.DataAnnotations;

namespace LibraryProject.DTO_s
{
    public class AuthorRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "Firstname must not contain more than 32 chars")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Lastname must not contain more than 32 chars")]
        public string LastName { get; set; }

        [StringLength(32, ErrorMessage = "Middlename must not contain more than 32 chars")]
        public string MiddleName { get; set; } = "";
    }
}
