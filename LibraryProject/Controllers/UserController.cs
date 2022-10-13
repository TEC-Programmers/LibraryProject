using LibraryProject.API.DTO_s;
using LibraryProject.API.Services;
using LibraryProject.API.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using LibraryProject.API.Helpers;
using System.Collections.Generic;
using LibraryProject.Database.Entities;
using LibraryProject.API.Repositories;
using System.Data.SqlClient;
using System.Data;

namespace LibraryProject.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _UsersService;

        public UsersController(IUserService UsersService)
        {
            _UsersService = UsersService;
        }

        //[Authorize(Role.Administrator)] // only admins are allowed entry to this endpoint
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
               
                List<UsersResponse> Userss = await _UsersService.GetAll();

                if (Userss == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (Userss.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Userss);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        //[Authorize(Role.Customer, Role.Administrator)]
        [HttpPost("registerWithProcedure")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterWithProcedure([FromBody] UsersRequest newUsers)
        {
            try
            {              
                UsersResponse Users = await _UsersService.registerWithProcedure(newUsers);
                return Ok(Users);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authenticate(LoginRequest login)
        {
            try
            {
                LoginResponse response = await _UsersService.Authenticate(login);

                if (response == null)
                {
                    return Unauthorized();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        //[Authorize(Role.Administrator)]
        [AllowAnonymous]
        [HttpGet("{UsersId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetById([FromRoute] int UsersId)
        {

            try
            {

                UsersResponse Users = await _UsersService.GetById(UsersId);

                if (Users == null)
                {
                    return NotFound();
                }

                return Ok(Users);

                UsersResponse currentUsers = (UsersResponse)HttpContext.Items["Users"];

                if (UsersId != currentUsers.Id && currentUsers.Role != Role.Administrator)
                {
                    return Unauthorized(new { message = "Unauthorized" });      // only admins can access other Users records
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }      

        }



        [AllowAnonymous]
        [HttpPut("updateUsersPassword/{UsersId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePassword([FromRoute] int UsersId, [FromBody] UsersRequest updateUsers)
        {
            try
            {
                UsersResponse Users = await _UsersService.UpdatePasswordWithProcedure(UsersId, updateUsers);

                if (Users == null)
                {
                    return Problem("Users record didn't get updated, something went wrong.");
                }

                return Ok(Users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPut("updateUsersRole/{UsersId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRole([FromRoute] int UsersId, [FromBody] UsersRequest updateUsers)
        {
            try
            {
                UsersResponse Users = await _UsersService.UpdateRoleWithProcedure(UsersId, updateUsers);

                if (Users == null)
                {
                    return Problem("Users record didn't get updated, something went wrong.");
                }

                return Ok(Users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }




        //update
        //[Authorize(Role.Customer, Role.Administrator)]
        [AllowAnonymous]
        [HttpPut("{UsersId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int UsersId, [FromBody] UsersRequest updateUsers)
        {
            try
            {
                UsersResponse Users = await _UsersService.UpdateProfileWithProcedure(UsersId, updateUsers);

                if (Users == null)
                {
                    return Problem("Users record didn't get updated, something went wrong.");
                }

                return Ok(Users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]

       // [Authorize(Role.Administrator)]
        [HttpDelete("{UsersId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int UsersId)
        {

            try
            {
               UsersResponse result = await _UsersService.Delete(UsersId);

                if (result == null)
                {
                    return NotFound();// Problem("Customer was not deleted, something went wrong");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


    }
}