using IMS.Api.DTOs.User;
using IMS.Api.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListUserDto>>> GetUsers()
        {
            var users = await _service.GetAllAsync();

            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _service.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User not found."
                });
            }

            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(
            [FromBody] CreateUserDto dto)
        {
            try
            {
                var user =
                    await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetUser),
                    new { id = user.Id },
                    user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDto>> UpdateUser(
            int id,
            [FromBody] UpdateUserDto dto)
        {
            try
            {
                var user =
                    await _service.UpdateAsync(id, dto);

                if (user == null)
                {
                    return NotFound(new
                    {
                        message = "User not found."
                    });
                }

                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted =
                await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "User not found."
                });
            }

            return NoContent();
        }
    }
}