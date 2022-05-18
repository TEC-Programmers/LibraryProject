using System.ComponentModel.DataAnnotations;

namespace LibraryProject.DTO_s
{
    public class LoanRequest
    {
        public int userID { get; set; }
        public int bookId { get; set; }
        [Required]
        public string loaned_At { get; set; }
        [Required]
        public string return_date { get; set; }
    }
}
