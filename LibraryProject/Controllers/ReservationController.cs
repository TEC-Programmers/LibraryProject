using LibraryProject.DTO_s;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
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
    }
}
