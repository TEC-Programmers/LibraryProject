﻿namespace LibraryProject.API.DTO_s
{
    public class LoanRequest
    {
        public int userID { get; set; }
        public int bookId { get; set; }
        public string loaned_At { get; set; }
        public string return_date { get; set; }
    }
}
