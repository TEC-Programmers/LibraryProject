namespace LibraryProject.API.DTO_s
{
    public class LoanResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int bookId { get; set; }
        public string loaned_At { get; set; }
        public string return_date { get; set; }
    }
}
