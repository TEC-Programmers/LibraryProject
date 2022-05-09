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
    }
}
