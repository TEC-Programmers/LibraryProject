namespace LibraryProject.API.DTO
{
    public class LoanRequest
    {
        public int UsersId { get; set; }
        public int bookId { get; set; }
        public string loaned_At { get; set; }
        public string return_date { get; set; }
    }
}
