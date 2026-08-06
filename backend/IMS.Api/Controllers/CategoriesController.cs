using IMS.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories
                .Where(x => !x.IsDeleted)
                .Select(x => new
                {
                    x.Id,
                    x.CategoryCode,
                    x.Name,
                    x.Slug,
                    x.Description,
                    x.IsActive
                })
                .ToListAsync();

            return Ok(categories);
        }
    }
}