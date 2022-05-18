using System.ComponentModel.DataAnnotations;

<<<<<<< HEAD
namespace LibraryProject.API.Database.Entities
=======
namespace LibraryProject.Database.Entities
>>>>>>> Bilal_Branch
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        public int userID { get; set; }
        public int bookId { get; set; }
        public string loaned_At { get; set; }
        public string return_date { get; set; }
    }
}
