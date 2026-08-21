using IMS.Api.Data;
using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IMS.Api.Repositories.Permissions
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _context.Permissions
                .AsNoTracking()
                .OrderBy(p => p.Module)
                .ThenBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Permission>> GetByIdsAsync(
            IEnumerable<int> ids)
        {
            var idList = ids.ToList();

            return await _context.Permissions
                .Where(p => idList.Contains(p.Id))
                .ToListAsync();
        }

        public async Task<Permission?> GetByNameAsync(
            string name)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
        }

        public void Update(Permission permission)
        {
            _context.Permissions.Update(permission);
        }

        public void Delete(Permission permission)
        {
            _context.Permissions.Remove(permission);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}