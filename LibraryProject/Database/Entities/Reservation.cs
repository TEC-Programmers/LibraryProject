using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;


namespace LibraryProject.Database.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }

        public int userId { get; set; }
        [ForeignKey("userId")] 
        public User User { get; set; }

        public int bookId { get; set; }
        [ForeignKey("bookId")] 
        public Book Book { get; set; }

        public string reserved_At { get; set; }
        public string reserved_To { get; set; }

    }
}
