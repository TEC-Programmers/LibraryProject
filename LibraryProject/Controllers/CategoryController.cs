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
    public class CategoryController : ControllerBase   // This class is inheriting class ControlleBase and implement the methods
    {
        private readonly ICategoryService _categoryService;     //making  instances of the class ICategoryService,
        public CategoryController(ICategoryService categoryService)   //dependency injection with parameter
        {
            _categoryService = categoryService;
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
                List<CategoryResponse> categoryResponse = await _categoryService.GetAllCategories();
                if (categoryResponse == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }
                if (categoryResponse.Count == 0)
                {
                    return NoContent();
                }
                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);

            }


        }

        // HTTPGET method by CategoryID  for requesting data from the server 

        // https://localhost:5001/api/Category/derp
        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int categoryId)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.GetCategoryById(categoryId);

                if (categoryResponse == null)
                {
                    return NotFound();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        // HTTPGET method only for Categories(without Books) for requesting data from the server 
        // https://localhost:5001/api/Category/derp
        [HttpGet("WithoutBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCategoriesWithoutProducts()
        {
            try
            {
                List<CategoryResponse> categoryResponse = await _categoryService.GetAllCategoriesWithoutBooks();
                if (categoryResponse == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }
                if (categoryResponse.Count == 0)
                {
                    return NoContent();
                }
                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);

            }


        }

        //HTTPPOST method for sending data to the server from an http client
        [HttpPost("WithProcedure")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateWithProcedure([FromBody] CategoryRequest newCategory)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.CreateCategoryWithProcedure(newCategory);

                if (categoryResponse == null)
                {
                    return Problem("Category Was not created, something went wrong");
                }

                return Ok(categoryResponse);
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
        public async Task<IActionResult> Create([FromBody] CategoryRequest newCategory)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.CreateCategory(newCategory);

                if (categoryResponse == null)
                {
                    return Problem("Category Was not created, something went wrong");
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        //HTTPPUT method to update an existing resource on the server
        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int categoryId, [FromBody] CategoryRequest updateAuthor)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.UpdateCategory(categoryId, updateAuthor);

                if (categoryResponse == null)
                {
                    return NotFound();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        //HTTPDelete method to delete a resource from the server
        [HttpDelete("WithProcedure/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteWithProcedure([FromRoute] int categoryId)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.DeleteCategoryWithProcedure(categoryId);

                if (categoryResponse == null)
                {
                    return NotFound();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int categoryId)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.Delete(categoryId);

                if (categoryResponse == null)
                {
                    return NotFound();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

    }
}
