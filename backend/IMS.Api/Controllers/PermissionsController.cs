using IMS.Api.DTOs.Permission;
using IMS.Api.Services.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _service;

        public PermissionsController(
            IPermissionService service)
        {
            _service = service;
        }

        // GET: api/Permissions
        [HttpGet]
        public async Task<
            ActionResult<IEnumerable<ListPermissionDto>>>
            GetPermissions()
        {
            var permissions =
                await _service.GetAllAsync();

            return Ok(new
            {
                message =
                    "Permissions retrieved successfully.",
                data = permissions
            });
        }

        // GET: api/Permissions/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PermissionDto>>
            GetPermission(int id)
        {
            var permission =
                await _service.GetByIdAsync(id);

            if (permission == null)
            {
                return NotFound(new
                {
                    message = "Permission not found."
                });
            }

            return Ok(new
            {
                message =
                    "Permission retrieved successfully.",
                data = permission
            });
        }

        // POST: api/Permissions
        [HttpPost]
        public async Task<ActionResult<PermissionDto>>
            CreatePermission(
                [FromBody] CreatePermissionDto dto)
        {
            try
            {
                var permission =
                    await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetPermission),
                    new { id = permission.Id },
                    new
                    {
                        message =
                            "Permission created successfully.",
                        data = permission
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

        // PUT: api/Permissions/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<PermissionDto>>
            UpdatePermission(
                int id,
                [FromBody] UpdatePermissionDto dto)
        {
            try
            {
                var permission =
                    await _service.UpdateAsync(id, dto);

                if (permission == null)
                {
                    return NotFound(new
                    {
                        message = "Permission not found."
                    });
                }

                return Ok(new
                {
                    message =
                        "Permission updated successfully.",
                    data = permission
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

        // DELETE: api/Permissions/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult>
            DeletePermission(int id)
        {
            var deleted =
                await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Permission not found."
                });
            }

            return Ok(new
            {
                message =
                    $"Permission {id} deleted successfully."
            });
        }
    }
}