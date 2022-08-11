using LibraryProject.API.Helpers;
using System.ComponentModel.DataAnnotations;


namespace LibraryProject.API.Database.Entities

{ //Creating Loan Entity for the database
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        public string loaned_At { get; set; }
        public string return_date { get; set; }
    }
}
