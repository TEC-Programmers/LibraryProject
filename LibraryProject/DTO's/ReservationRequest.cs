namespace LibraryProject.API.DTO_s
{
    public class ReservationRequest
    {
        public int UsersId { get; set; }
        public int bookId { get; set; }
        public string reserved_At { get; set; }
        public string reserved_To { get; set; }
    }
}
