using IMS.Api.Models;

// The repository defines what database operations are available.
namespace IMS.Api.Repositories.Brands
{
    public interface IBrandRepository
    {
        Task<List<Brand>> GetAllAsync();

        Task<Brand?> GetByIdAsync(int id);

        Task<Brand?> GetByCodeAsync(string brandCode);

        Task AddAsync(Brand brand);

        void Update(Brand brand);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}