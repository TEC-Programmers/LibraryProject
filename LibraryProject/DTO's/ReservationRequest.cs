namespace LibraryProject.DTO_s
{
    public class Reservationrequest
    {
        public int reservationId { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        public string reserved_At { get; set; }
        public string reserved_To { get; set; }
    }
}
