using IMS.Api.DTOs.Brand;
using IMS.Api.Services.Brands;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

// Your controller becomes much cleaner.
namespace IMS.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _service;

        public BrandsController(IBrandService service)
        {
            _service = service;
        }

        // GET: api/brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListBrandDto>>> GetBrands()
        {
            var brands = await _service.GetAllAsync();

            return Ok(brands);
        }

        // GET: api/brands/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BrandDto>> GetBrand(int id)
        {
            var brand = await _service.GetByIdAsync(id);

            if (brand == null)
                return NotFound(new
                {
                    message = "Brand not found."
                });

            return Ok(brand);
        }

        // POST: api/brands
        [HttpPost]
        public async Task<ActionResult<BrandDto>> CreateBrand(
            [FromForm] CreateBrandDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                var brand =
                    await _service.CreateAsync(dto, userId);

                return CreatedAtAction(
                    nameof(GetBrand),
                    new { id = brand.Id },
                    brand);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // PUT: api/brands/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<BrandDto>> UpdateBrand(
            int id,
            [FromForm] UpdateBrandDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                var brand =
                    await _service.UpdateAsync(
                        id,
                        dto,
                        userId);

                if (brand == null)
                {
                    return NotFound(new
                    {
                        message = "Brand not found."
                    });
                }

                return Ok(brand);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/brands/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var userId = GetCurrentUserId();

            var deleted =
                await _service.DeleteAsync(id, userId);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Brand not found."
                });
            }

            return NoContent();
        }

        private int GetCurrentUserId()
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userId, out var id))
            {
                throw new UnauthorizedAccessException(
                    "User is not authenticated.");
            }

            return id;
        }
    }
}