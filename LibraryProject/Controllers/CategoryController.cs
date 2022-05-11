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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
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
    }
}
