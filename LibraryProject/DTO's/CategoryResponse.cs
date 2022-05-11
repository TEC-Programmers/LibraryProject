using System.Collections.Generic;

namespace LibraryProject.API.DTO_s
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public List<CategoryBookResponse> Books { get; set; } = new();
    }


    public class CategoryBookResponse 
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public string Language { get; set; }

        public string Description { get; set; }


        public int PublishYear { get; set; }

    }
}
