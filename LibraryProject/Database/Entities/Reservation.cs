using System.ComponentModel.DataAnnotations;

<<<<<<< HEAD
namespace LibraryProject.API.Database.Entities
=======
namespace LibraryProject.Database.Entities
>>>>>>> Fahim's-Branch
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        public string reserved_At { get; set; }
        public string reserved_To { get; set; }
<<<<<<< HEAD
=======

>>>>>>> Fahim's-Branch
    }
}
