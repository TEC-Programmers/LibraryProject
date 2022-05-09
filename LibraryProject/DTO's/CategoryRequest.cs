using System.ComponentModel.DataAnnotations;

namespace LibraryProject.API.DTO_s
{
    public class CategoryRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "CategoryName can notbe more than 32 chars")]

        public string CategoryName { get; set; }

    }
}
