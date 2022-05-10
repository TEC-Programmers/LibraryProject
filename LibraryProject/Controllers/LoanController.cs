using LibraryProject.DTO_s;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<LoanResponse> loans = _loanService.GetAllLoans();
                if (loans == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (loans.Count == 0)
                {
                    return NoContent();
                }

                return Ok(loans);

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
           
        }
    }
}
