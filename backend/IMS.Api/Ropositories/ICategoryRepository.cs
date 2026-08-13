using IMS.Api.Models;

//The repository defines what database operations are available.
namespace IMS.Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task<Category?> GetByCodeAsync(string categoryCode);

        Task AddAsync(Category category);

        void Update(Category category);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}