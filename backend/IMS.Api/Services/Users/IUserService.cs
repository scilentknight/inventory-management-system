using IMS.Api.DTOs.User;

namespace IMS.Api.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<ListUserDto>> GetAllAsync();

        Task<UserDto?> GetByIdAsync(int id);

        Task<UserDto> CreateAsync(CreateUserDto dto);

        Task<UserDto?> UpdateAsync(
            int id,
            UpdateUserDto dto);

        Task<bool> DeleteAsync(int id);
    }
}