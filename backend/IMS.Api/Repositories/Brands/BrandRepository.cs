using IMS.Api.Data;
using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;

// This is where your actual Entity Framework Core database operations happen.
namespace IMS.Api.Repositories.Brands
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            return await _context.Brands
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            return await _context.Brands
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);
        }

        public async Task<Brand?> GetByCodeAsync(string brandCode)
        {
            return await _context.Brands
                .FirstOrDefaultAsync(x =>
                    x.BrandCode == brandCode &&
                    !x.IsDeleted);
        }

        public async Task AddAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
        }

        public void Update(Brand brand)
        {
            _context.Brands.Update(brand);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Brands
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