using IMS.Api.Models;

namespace IMS.Api.Repositories.Auths
{
    public interface IAuthRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(int id);

        Task AddAsync(User user);

        Task SaveChangesAsync();
    }
}