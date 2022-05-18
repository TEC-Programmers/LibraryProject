using LibraryProject.API.Services;
using LibraryProject.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorservice;

        public AuthorController(IAuthorService authorService)
        {
            _authorservice = authorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getAll()
        {
            try
            {
                List<AuthorResponse> authors = await _authorservice.GetAllAuthors();

                if (authors == null)
                {
                    return Problem("Got no data, not even empty list, this is unexpected");
                }

                if (authors.Count == 0)
                {
                    return NoContent();
                }

                return Ok(authors);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpGet("{authorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int authorId)
        {
            try
            {
                AuthorResponse authorResponse = await _authorservice.GetAuthorById(authorId);

                if (authorResponse == null)
                {
                    return NotFound();
                }

                return Ok(authorResponse);

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromBody] AuthorRequest newAuthor)
        {
            try
            {
                AuthorResponse authorResponse = await _authorservice.CreateAuthor(newAuthor);

                if (authorResponse == null)
                {
                    return Problem(" Author was NOT created, something went wrong");
                }

                return Ok(authorResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{authorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int authorId, [FromBody] AuthorRequest updateAuthor)
        {
            try
            {
                AuthorResponse authorResponse = await _authorservice.UpdateAuthor(authorId, updateAuthor);

                if (authorResponse == null)
                {
                    return NotFound();
                }

                return Ok(authorResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{authorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int authorId)
        {
            try
            {
                AuthorResponse authorResponse = await _authorservice.DeleteAuthor(authorId);

                if (authorResponse == null)
                {
                    return NotFound();
                }

                return Ok(authorResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
