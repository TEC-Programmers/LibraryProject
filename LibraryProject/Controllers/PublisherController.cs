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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService PublisherService)
        {
            _publisherService = PublisherService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getAll()
        {
            try
            {
                List<PublisherResponse> Publishers = await _publisherService.GetAllPublishers();

                if (Publishers == null)
                {
                    return Problem("Got no data, not even empty list, this is unexpected");
                }

                if (Publishers.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Publishers);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpGet("{publisherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int publisherId)
        {
            try
            {
                PublisherResponse PublisherResponse = await _publisherService.GetPublisherById(publisherId);

                if (PublisherResponse == null)
                {
                    return NotFound();
                }

                return Ok(PublisherResponse);

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPost("WithProcedure")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateWithProcedure([FromBody] PublisherRequest newPublisher)
        {
            try
            {
                PublisherResponse PublisherResponse = await _publisherService.CreatePublisherWithProcedure(newPublisher);

                if (PublisherResponse == null)
                {
                    return Problem(" Publisher was NOT created, something went wrong");
                }

                return Ok(PublisherResponse);
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
        public async Task<ActionResult> Create([FromBody] PublisherRequest newPublisher)
        {
            try
            {
                PublisherResponse PublisherResponse = await _publisherService.CreatePublisher(newPublisher);

                if (PublisherResponse == null)
                {
                    return Problem(" Publisher was NOT created, something went wrong");
                }

                return Ok(PublisherResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{publisherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int publisherId, [FromBody] PublisherRequest updatePublisher)
        {
            try
            {
                PublisherResponse PublisherResponse = await _publisherService.UpdatePublisher(publisherId, updatePublisher);

                if (PublisherResponse == null)
                {
                    return NotFound();
                }

                return Ok(PublisherResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("WithProcedure/{publisherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteWithProcedure([FromRoute] int publisherId)
        {
            try
            {
                PublisherResponse PublisherResponse = await _publisherService.DeletePublisherWithProcedure(publisherId);

                if (PublisherResponse == null)
                {
                    return NotFound();
                }

                return Ok(PublisherResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{publisherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int publisherId)
        {
            try
            {
                PublisherResponse PublisherResponse = await _publisherService.DeletePublisher(publisherId);

                if (PublisherResponse == null)
                {
                    return NotFound();
                }

                return Ok(PublisherResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
