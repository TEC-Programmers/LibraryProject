using System.ComponentModel.DataAnnotations;

namespace LibraryProject.API.Database.Entities
{
    public class Reservation
    {
        [Key]
        public int reservationId { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        public string reserved_At { get; set; }
        public string reserved_To { get; set; }
    }
}
