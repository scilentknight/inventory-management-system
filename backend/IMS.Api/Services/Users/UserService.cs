using IMS.Api.DTOs.User;
using IMS.Api.Models;
using IMS.Api.Repositories.Users;

namespace IMS.Api.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ListUserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            return users.Select(user => new ListUserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            });
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                return null;

            return MapToDto(user);
        }

        public async Task<UserDto> CreateAsync(
            CreateUserDto dto)
        {
            var email = dto.Email.Trim();

            // Check duplicate email
            var existingUser =
                await _repository.GetByEmailAsync(email);

            if (existingUser != null)
            {
                throw new InvalidOperationException(
                    "A user with this email already exists.");
            }

            // Hash password
            var passwordHash =
                BCrypt.Net.BCrypt.HashPassword(
                    dto.Password);

            var user = new User
            {
                Email = email,
                PasswordHash = passwordHash,
                Role = dto.Role.Trim(),
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(user);

            await _repository.SaveChangesAsync();

            // Reload user so generated Id is available
            var createdUser =
                await _repository.GetByIdAsync(user.Id);

            return MapToDto(createdUser!);
        }

        public async Task<UserDto?> UpdateAsync(
            int id,
            UpdateUserDto dto)
        {
            var user =
                await _repository.GetByIdAsync(id);

            if (user == null)
                return null;

            var email = dto.Email.Trim();

            // Check whether another user already uses email
            var existingUser =
                await _repository.GetByEmailAsync(email);

            if (existingUser != null &&
                existingUser.Id != id)
            {
                throw new InvalidOperationException(
                    "A user with this email already exists.");
            }

            user.Email = email;
            user.Role = dto.Role.Trim();
            user.IsActive = dto.IsActive;

            _repository.Update(user);

            await _repository.SaveChangesAsync();

            return MapToDto(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user =
                await _repository.GetByIdAsync(id);

            if (user == null)
                return false;

            _repository.Delete(user);

            await _repository.SaveChangesAsync();

            return true;
        }

        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }
    }
}