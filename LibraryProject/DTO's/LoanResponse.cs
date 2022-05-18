<<<<<<< HEAD
﻿namespace LibraryProject.API.DTO
=======
﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.DTO_s
>>>>>>> Bilal_Branch
{
    public class LoanResponse
    {
        public int Id { get; set; }
        public int userID { get; set; }
        public int bookId { get; set; }
<<<<<<< HEAD
        public string loaned_At { get; set; }
=======

        public string loaned_At { get; set; }

>>>>>>> Bilal_Branch
        public string return_date { get; set; }
    }
}
