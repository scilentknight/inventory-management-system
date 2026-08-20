using IMS.Api.DTOs.Product;
using IMS.Api.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IMS.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListProductDto>>> GetProducts()
        {
            var products = await _service.GetAllAsync();

            return Ok(new
            {
                message = "Products retrieved successfully.",
                data = products
            });
        }

        // GET: api/products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Product not found."
                });
            }

            return Ok(new
            {
                message = "Product retrieved successfully.",
                data = product
            });
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(
            [FromForm] CreateProductDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                var product =
                    await _service.CreateAsync(dto, userId);

                return CreatedAtAction(
                    nameof(GetProduct),
                    new { id = product.Id },
                    new
                    {
                        message = "Product created successfully.",
                        data = product
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

        // PUT: api/products/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(
            int id,
            [FromForm] UpdateProductDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                var product =
                    await _service.UpdateAsync(
                        id,
                        dto,
                        userId);

                if (product == null)
                {
                    return NotFound(new
                    {
                        message = "Product not found."
                    });
                }

                return Ok(new
                {
                    message = "Product updated successfully.",
                    data = product
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

        // PATCH: api/products/5
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<ProductDto>> PatchProduct(
            int id,
            [FromForm] PatchProductDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();

                var product =
                    await _service.PatchAsync(
                        id,
                        dto,
                        userId);

                if (product == null)
                {
                    return NotFound(new
                    {
                        message = "Product not found."
                    });
                }

                return Ok(new
                {
                    message = "Product updated successfully.",
                    data = product
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

        // DELETE: api/products/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var userId = GetCurrentUserId();

            var deleted =
                await _service.DeleteAsync(id, userId);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Product not found."
                });
            }

            return Ok(new
            {
                message = $"Product {id} deleted successfully."
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