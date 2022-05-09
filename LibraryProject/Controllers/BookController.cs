using LibraryProject.API.DTO_s;
using LibraryProject.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookService _bookService;



        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<BookResponse> productResponses = await _bookService.GetAllBooks();
                if (productResponses == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }
                if (productResponses.Count == 0)
                {
                    return NoContent();
                }
                return Ok(productResponses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);

            }


        }
    }
}
