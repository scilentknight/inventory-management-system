using IMS.Api.DTOs.Unit;
using IMS.Api.Services.Units;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IMS.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService _service;

        public UnitsController(IUnitService service)
        {
            _service = service;
        }

        // GET: api/units
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListUnitDto>>> GetUnits()
        {
            var units = await _service.GetAllAsync();

            return Ok(new
            {
                message = "Units retrieved successfully.",
                data = units
            });
        }

        // GET: api/units/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UnitDto>> GetUnit(int id)
        {
            var unit = await _service.GetByIdAsync(id);

            if (unit == null)
            {
                return NotFound(new
                {
                    message = "Unit not found."
                });
            }

            return Ok(new
            {
                message = "Unit retrieved successfully.",
                data = unit
            });
        }

        // POST: api/units
        [HttpPost]
        public async Task<ActionResult<UnitDto>> CreateUnit(
            [FromBody] CreateUnitDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                var unit =
                    await _service.CreateAsync(dto, userId);

                return CreatedAtAction(
                    nameof(GetUnit),
                    new { id = unit.Id },
                    new
                    {
                        message = "Unit created successfully.",
                        data = unit
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

        // PUT: api/units/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UnitDto>> UpdateUnit(
            int id,
            [FromBody] UpdateUnitDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                var unit =
                    await _service.UpdateAsync(
                        id,
                        dto,
                        userId);

                if (unit == null)
                {
                    return NotFound(new
                    {
                        message = "Unit not found."
                    });
                }

                return Ok(new
                {
                    message = "Unit updated successfully.",
                    data = unit
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

        // PATCH: api/units/5
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<UnitDto>> PatchUnit(
            int id,
            [FromBody] PatchUnitDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                var unit =
                    await _service.PatchAsync(
                        id,
                        dto,
                        userId);

                if (unit == null)
                {
                    return NotFound(new
                    {
                        message = "Unit not found."
                    });
                }

                return Ok(new
                {
                    message = "Unit updated successfully.",
                    data = unit
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

        // DELETE: api/units/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            try
            {
                var userId = GetCurrentUserId();

                var deleted =
                    await _service.DeleteAsync(
                        id,
                        userId);

                if (!deleted)
                {
                    return NotFound(new
                    {
                        message = "Unit not found."
                    });
                }

                return Ok(new
                {
                    message = $"Unit {id} deleted successfully."
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

        private int GetCurrentUserId()
        {
            var userId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            if (!int.TryParse(userId, out var id))
            {
                throw new UnauthorizedAccessException(
                    "User is not authenticated.");
            }

            return id;
        }
    }
}