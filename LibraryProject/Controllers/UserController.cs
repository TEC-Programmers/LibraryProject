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

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize(Role.Administrator)] // only admins are allowed entry to this endpoint
        [AllowAnonymous]
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
               
                List<UserResponse> users = await _userService.GetAll();

                if (users == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (users.Count == 0)
                {
                    return NoContent();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost("WithProcedure")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterWithProcedure([FromBody] UserRequest newUser)
        {
            try
            {              
                UserResponse user = await _userService.registerWithProcedure(newUser);
                return Ok(user);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserRequest newUser)
        {
            try
            {
                UserResponse user = await _userService.register(newUser);
                return Ok(user);

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
                LoginResponse response = await _userService.Authenticate(login);

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



        [AllowAnonymous]
        [HttpGet("WithProcedure/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetByIdWithProcedure([FromRoute] int userId)
        {

            try
            {

                UserResponse user = await _userService.GetByIdWithProcedure(userId);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);

                UserResponse currentUser = (UserResponse)HttpContext.Items["User"];

                if (userId != currentUser.Id && currentUser.Role != Role.Administrator)
                {
                    return Unauthorized(new { message = "Unauthorized" });      // only admins can access other user records
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }


        [AllowAnonymous]
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetById([FromRoute] int userId)
        {

            try
            {

                UserResponse user = await _userService.GetById(userId);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);

                //UserResponse currentUser = (UserResponse)HttpContext.Items["User"];

                //if (userId != currentUser.Id && currentUser.Role != Role.Administrator)
                //{
                //    return Unauthorized(new { message = "Unauthorized" });      // only admins can access other user records
                //}
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }


        [AllowAnonymous]
        [HttpPut("updateUserPassword/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePassword([FromRoute] int userId, [FromBody] UserRequest updateUser)
        {
            try
            {
                UserResponse user = await _userService.UpdatePasswordWithProcedure(userId, updateUser);

                if (user == null)
                {
                    return Problem("User record didn't get updated, something went wrong.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPut("updateUserRole/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRole([FromRoute] int userId, [FromBody] UserRequest updateUser)
        {
            try
            {
                UserResponse user = await _userService.UpdateRoleWithProcedure(userId, updateUser);

                if (user == null)
                {
                    return Problem("User record didn't get updated, something went wrong.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        //[Authorize(Role.Customer, Role.Administrator)]
        [AllowAnonymous]
        [HttpPut("updateUserProfile/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProfile([FromRoute] int userId, [FromBody] UserRequest updateUser)
        {
            try
            {
                UserResponse user = await _userService.UpdateProfileWithProcedure(userId, updateUser);

                if (user == null)
                {
                    return Problem("User record didn't get updated, something went wrong.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int userId, [FromBody] UserRequest updateUser)
        {
            try
            {
                UserResponse user = await _userService.Update(userId, updateUser);

                if (user == null)
                {
                    return Problem("User record didn't get updated, something went wrong.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[Authorize(Role.Administrator)]
        [AllowAnonymous]
        [HttpDelete("WithProcedure/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteWithProcedure([FromRoute] int userId)
        {

            try
            {
               UserResponse result = await _userService.DeleteWithProcedure(userId);

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

        [AllowAnonymous]
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int userId)
        {

            try
            {
                UserResponse result = await _userService.Delete(userId);

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