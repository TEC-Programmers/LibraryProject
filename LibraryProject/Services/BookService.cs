namespace LibraryProject.API.Services
{

    public interface IBookService
    {
        Task<List<BookResponse>> GetAllBooks();
    }

        public class BookService:IBookService
    {


    }
}
