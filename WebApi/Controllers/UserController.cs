using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACCWebApp.Models;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetAllUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser(Users users)
        {
            await _userService.AddUserAsync(users);
            return CreatedAtAction(nameof(GetUserById), new { id = users.UserId }, users);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> UpdateUser(int id, Users users)
        {
            if (id != users.UserId)
                return BadRequest();

            await _userService.UpdateUserAsync(users);
            return NoContent();
        }
        ///
        ///////////////////////////////////////////////////
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var users = await _userService.GetUserByIdAsync(id);
            if (users == null)
                return NotFound();

            await _userService.DeleteUserAsync(users);
            return NoContent();
        }
    }
}
