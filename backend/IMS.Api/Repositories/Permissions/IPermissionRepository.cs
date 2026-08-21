using IMS.Api.Models;

namespace IMS.Api.Repositories.Permissions
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();

        Task<Permission?> GetByIdAsync(int id);

        Task<List<Permission>> GetByIdsAsync(
            IEnumerable<int> ids);

        Task<Permission?> GetByNameAsync(string name);

        Task AddAsync(Permission permission);

        void Update(Permission permission);

        void Delete(Permission permission);

        Task SaveChangesAsync();
    }
}