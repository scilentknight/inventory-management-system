using IMS.Api.Models;

namespace IMS.Api.Repositories.Units
{
    // Defines database operations available for units
    public interface IUnitRepository
    {
        Task<List<Unit>> GetAllAsync();

        Task<Unit?> GetByIdAsync(int id);

        Task<Unit?> GetByCodeAsync(string unitCode);

        Task<Unit?> GetByNameAsync(string name);

        Task<Unit?> GetByShortNameAsync(string shortName);

        Task AddAsync(Unit unit);

        void Update(Unit unit);

        Task<bool> ExistsAsync(int id);

        Task<bool> HasProductsAsync(int id);

        Task SaveChangesAsync();
    }
}