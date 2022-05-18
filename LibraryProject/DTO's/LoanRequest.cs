<<<<<<< HEAD
﻿namespace LibraryProject.API.DTO
{
    public class LoanRequest
    {
        public int userID { get; set; }
        public int bookId { get; set; }
        public string loaned_At { get; set; }
=======
﻿using System.ComponentModel.DataAnnotations;

namespace LibraryProject.DTO_s
{
    public class LoanRequest
    {
        public int userID { get; set; }

        public int bookId { get; set; }

        //[Required]
       public string loaned_At { get; set; }

        //[Required]
>>>>>>> Bilal_Branch
        public string return_date { get; set; }
    }
}
