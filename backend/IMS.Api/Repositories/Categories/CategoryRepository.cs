using IMS.Api.Data;
using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;

// This is where your actual Entity Framework Core database operations happen.
namespace IMS.Api.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Where(x => !x.IsDeleted)
                .Include(x => x.ParentCategory)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(x => x.ParentCategory)
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);
        }

        public async Task<Category?> GetByCodeAsync(string categoryCode)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(x =>
                    x.CategoryCode == categoryCode &&
                    !x.IsDeleted);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Categories
                .AnyAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}