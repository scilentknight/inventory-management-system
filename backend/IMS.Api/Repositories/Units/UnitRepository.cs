using IMS.Api.Data;
using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IMS.Api.Repositories.Units
{
    // Handles actual Entity Framework Core database operations
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext _context;

        public UnitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Unit>> GetAllAsync()
        {
            return await _context.Units
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Unit?> GetByIdAsync(int id)
        {
            return await _context.Units
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);
        }

        public async Task<Unit?> GetByCodeAsync(string unitCode)
        {
            return await _context.Units
                .FirstOrDefaultAsync(x =>
                    x.UnitCode == unitCode &&
                    !x.IsDeleted);
        }

        public async Task<Unit?> GetByNameAsync(string name)
        {
            return await _context.Units
                .FirstOrDefaultAsync(x =>
                    x.Name == name &&
                    !x.IsDeleted);
        }

        public async Task<Unit?> GetByShortNameAsync(string shortName)
        {
            return await _context.Units
                .FirstOrDefaultAsync(x =>
                    x.ShortName == shortName &&
                    !x.IsDeleted);
        }

        public async Task AddAsync(Unit unit)
        {
            await _context.Units.AddAsync(unit);
        }

        public void Update(Unit unit)
        {
            _context.Units.Update(unit);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Units
                .AnyAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);
        }

        public async Task<bool> HasProductsAsync(int id)
        {
            return await _context.Products
                .AnyAsync(x =>
                    x.UnitId == id &&
                    !x.IsDeleted);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}