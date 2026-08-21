using IMS.Api.DTOs.Role;
using IMS.Api.Services.Roles;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListRoleDto>>>
            GetRoles()
        {
            var roles = await _service.GetAllAsync();

            return Ok(new
            {
                message = "Roles retrieved successfully.",
                data = roles
            });
        }

        // GET: api/Roles/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleDto>>
            GetRole(int id)
        {
            var role =
                await _service.GetByIdAsync(id);

            if (role == null)
            {
                return NotFound(new
                {
                    message = "Role not found."
                });
            }

            return Ok(new
            {
                message = "Role retrieved successfully.",
                data = role
            });
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<RoleDto>>
            CreateRole([FromBody] CreateRoleDto dto)
        {
            try
            {
                var role =
                    await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetRole),
                    new { id = role.Id },
                    new
                    {
                        message = "Role created successfully.",
                        data = role
                    });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // PUT: api/Roles/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<RoleDto>>
            UpdateRole(
                int id,
                [FromBody] UpdateRoleDto dto)
        {
            try
            {
                var role =
                    await _service.UpdateAsync(id, dto);

                if (role == null)
                {
                    return NotFound(new
                    {
                        message = "Role not found."
                    });
                }

                return Ok(new
                {
                    message = "Role updated successfully.",
                    data = role
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // PUT: api/Roles/5/permissions
        [HttpPut("{id:int}/permissions")]
        public async Task<ActionResult<RoleDto>>
            AssignPermissions(
                int id,
                [FromBody] AssignPermissionsDto dto)
        {
            try
            {
                var role =
                    await _service.AssignPermissionsAsync(
                        id,
                        dto);

                if (role == null)
                {
                    return NotFound(new
                    {
                        message = "Role not found."
                    });
                }

                return Ok(new
                {
                    message =
                        "Permissions assigned successfully.",
                    data = role
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult>
            DeleteRole(int id)
        {
            try
            {
                var deleted =
                    await _service.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound(new
                    {
                        message = "Role not found."
                    });
                }

                return Ok(new
                {
                    message =
                        $"Role {id} deleted successfully."
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}