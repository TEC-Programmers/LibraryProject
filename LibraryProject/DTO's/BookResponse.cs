namespace LibraryProject.API.DTO_s
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        
        public string Language { get; set; }
       
        public string Description { get; set; }

        
        public int PublishYear { get; set; }

       
        public int CategoryId { get; set; }

        //public int AuthorId { get; set; }

        public BookCategoryResponse Category { get; set; }

        public BookAuthorResponse Author { get; set; }
    }


    public class BookCategoryResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }

    public class BookAuthorResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }
    }
}
