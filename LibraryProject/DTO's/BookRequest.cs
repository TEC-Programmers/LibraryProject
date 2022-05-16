using System.ComponentModel.DataAnnotations;

namespace LibraryProject.API.DTO_s
{
    public class BookRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int PublishYear { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int PublisherId { get; set; }
    }
}
