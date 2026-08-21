using IMS.Api.Models;

namespace IMS.Api.Repositories.Roles
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();

        Task<Role?> GetByIdAsync(int id);

        Task<Role?> GetByNameAsync(string name);

        Task<bool> ExistsAsync(int id);

        Task AddAsync(Role role);

        void Update(Role role);

        void Delete(Role role);

        Task SaveChangesAsync();
    }
}