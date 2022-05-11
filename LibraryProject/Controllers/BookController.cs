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

        // https://localhost:5001/api/Product/derp
        [HttpGet("{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int bookId)
        {
            try
            {
                BookResponse bookResponse = await _bookService.GetBookById(bookId);

                if (bookResponse == null)
                {
                    return NotFound();
                }

                return Ok(bookResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
        // https://localhost:5001/api/Product/derp

        [HttpGet("Category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBooksByCategoryId([FromRoute] int categoryId)
        {
            try
            {
                List<BookResponse> productResponse = await _bookService.GetBooksByCategoryId(categoryId);
                if (productResponse == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }
                if (productResponse.Count == 0)
                {
                    return NoContent();
                }
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);

            }


        }

    }
}
