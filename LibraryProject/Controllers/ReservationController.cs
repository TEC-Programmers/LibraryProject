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
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<ReservationResponse> reservationResponses = await _reservationService.GetAllReservations();

                if (reservationResponses == null)
                {
                    return Problem("No data");
                }

                if (reservationResponses.Count == 0)
                {
                    return NoContent();
                }

                return Ok(reservationResponses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpGet("{reservationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int reservationId)
        {

            try
            {
                ReservationResponse reservationResponse = await _reservationService.GetReservationById(reservationId);

                if (reservationResponse == null)
                {
                    return NotFound();
                }

                return Ok(reservationResponse);
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
        public async Task<IActionResult> CreateWithProcedure([FromBody] ReservationRequest newReservation)
        {

            try
            {
                ReservationResponse reservationResponse = await _reservationService.CreateReservationWithProcedure(newReservation);

                if (reservationResponse == null)
                {
                    return Problem("Reservation could not be created, something went wrong");
                }

                return Ok(reservationResponse);
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
        public async Task<IActionResult> Create([FromBody] ReservationRequest newReservation)
        {

            try
            {
                ReservationResponse reservationResponse = await _reservationService.CreateReservation(newReservation);

                if (reservationResponse == null)
                {
                    return Problem("Reservation could not be created, something went wrong");
                }

                return Ok(reservationResponse);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPut("{reservationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int reservationId, [FromBody] ReservationRequest updateReservation)
        {

            try
            {
                ReservationResponse reservationResponse = await _reservationService.UpdateReservation(reservationId, updateReservation);

                if (reservationResponse == null)
                {
                    return NotFound();
                }

                return Ok(reservationResponse);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpDelete("WithProcedure/{reservationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteWithProcedure([FromRoute] int reservationId)
        {

            try
            {
                ReservationResponse reservationResponse = await _reservationService.DeleteReservationWithProcedure(reservationId);

                if (reservationResponse == null)
                {
                    return NotFound();
                }

                return Ok(reservationResponse);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpDelete("{reservationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int reservationId)
        {

            try
            {
                ReservationResponse reservationResponse = await _reservationService.DeleteReservation(reservationId);

                if (reservationResponse == null)
                {
                    return NotFound();
                }

                return Ok(reservationResponse);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
