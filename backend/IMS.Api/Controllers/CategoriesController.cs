using IMS.Api.DTOs.Category;
using IMS.Api.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IMS.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListCategoryDto>>> GetCategories()
        {
            var categories = await _service.GetAllAsync();

            return Ok(new
            {
                message = "Categories retrieved successfully.",
                data = categories
            });
        }

        // GET: api/categories/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _service.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound(new
                {
                    message = "Category not found."
                });
            }

            return Ok(new
            {
                message = "Category retrieved successfully.",
                data = category
            });
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(
            [FromForm] CreateCategoryDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                var category =
                    await _service.CreateAsync(dto, userId);

                return CreatedAtAction(
                    nameof(GetCategory),
                    new { id = category.Id },
                    new
                    {
                        message = "Category created successfully.",
                        data = category
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

        // PUT: api/categories/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(
            int id,
            [FromForm] UpdateCategoryDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                var category =
                    await _service.UpdateAsync(
                        id,
                        dto,
                        userId);

                if (category == null)
                {
                    return NotFound(new
                    {
                        message = "Category not found."
                    });
                }

                return Ok(new
                {
                    message = "Category updated successfully.",
                    data = category
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

        // PATCH: api/categories/5
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CategoryDto>> PatchCategory(
            int id,
            [FromForm] PatchCategoryDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                var category =
                    await _service.PatchAsync(
                        id,
                        dto,
                        userId);

                if (category == null)
                {
                    return NotFound(new
                    {
                        message = "Category not found."
                    });
                }

                return Ok(new
                {
                    message = "Category updated successfully.",
                    data = category
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

        // DELETE: api/categories/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var userId = GetCurrentUserId();

            var deleted =
                await _service.DeleteAsync(id, userId);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Category not found."
                });
            }

            return Ok(new
            {
                message = $"Category {id} deleted successfully."
            });
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