﻿using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Database.Entities
{
    public class Loan
    {
        [Key]
        public int loanId { get; set; }
        public int userID { get; set; }
        public int bookId { get; set; }
        public  string loaned_At { get; set; }
        public string return_date { get; set; }
        
        
    }
}
