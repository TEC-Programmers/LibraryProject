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
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<LoanResponse> loans = await _loanService.GetAllLoans();
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
        [HttpGet("{loanid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int loanid)
        {
            try
            {
                LoanResponse loanResponse = await _loanService.GetLoanById(loanid);
                if (loanResponse == null)
                {
                    return NotFound();
                }

                return Ok(loanResponse);

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
        public async Task<IActionResult> Create([FromBody] LoanRequest newLoan)
        {
            try
            {
                LoanResponse loanResponse = await _loanService.CreateLoan(newLoan);
                if (loanResponse == null)
                {
                    return Problem("Loan Was NOT created, something went wrong");
                }

                return Ok(loanResponse);

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }


        [HttpPut("{loanId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int loanId, [FromBody] LoanRequest updateLoan)
        {
            try
            {
                LoanResponse loanResponse = await _loanService.UpdateLoan(loanId, updateLoan);
                if (loanResponse == null)
                {
                    return NotFound();
                }

                return Ok(loanResponse);

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [HttpDelete("{loanid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int loanId)
        {
            try
            {
                LoanResponse loanResponse = await _loanService.DeleteLoan(loanId);
                if (loanResponse == null)
                {
                    return NotFound();
                }

                return Ok(loanResponse);

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
