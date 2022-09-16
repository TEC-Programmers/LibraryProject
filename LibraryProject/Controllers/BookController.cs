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
    public class BookController : ControllerBase   // This class is inheriting class ControlleBase and implement the methods
    {

        private readonly IBookService _bookService;   //making  instances of the class IBookService,

        public BookController(IBookService bookService)  //dependency injection with parameter
        {
            _bookService = bookService;
        }

        // HTTPGET method for requesting data from the server 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<BookResponse> bookResponses = await _bookService.GetAllBooks();  //getting 
                if (bookResponses == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }
                if (bookResponses.Count == 0)
                {
                    return NoContent();
                }
                return Ok(bookResponses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);

            }


        }

        // HTTPGET method by BookID  for requesting data from the server 

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

        // HTTPGET method by CategoryID  for requesting data from the server 
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


        //HTTPPOST method for sending data to the server from an http client
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BookRequest newBook)
        {
            try
            {
                BookResponse bookResponse = await _bookService.CreateBook(newBook);

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

        //HTTPPUT method to update an existing resource on the server
        [HttpPut("{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int bookId, [FromBody] BookRequest updateBook)
        {
            try
            {
                BookResponse bookResponse = await _bookService.UpdateBook(bookId, updateBook);

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

        //HTTPDelete method to delete a resource from the server
        [HttpDelete("{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int bookId)
        {
            try
            {
                BookResponse bookResponse = await _bookService.DeleteBook(bookId);

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

    }
}
