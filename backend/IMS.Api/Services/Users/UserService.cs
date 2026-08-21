//using IMS.Api.DTOs.User;
//using IMS.Api.Models;
//using IMS.Api.Repositories.Users;

//namespace IMS.Api.Services.Users
//{
//    public class UserService : IUserService
//    {
//        private readonly IUserRepository _repository;

//        public UserService(IUserRepository repository)
//        {
//            _repository = repository;
//        }

//        public async Task<IEnumerable<ListUserDto>> GetAllAsync()
//        {
//            var users = await _repository.GetAllAsync();

//            return users.Select(user => new ListUserDto
//            {
//                Id = user.Id,
//                Email = user.Email,
//                Role = user.Role,
//                IsActive = user.IsActive,
//                CreatedAt = user.CreatedAt
//            });
//        }

//        public async Task<UserDto?> GetByIdAsync(int id)
//        {
//            var user = await _repository.GetByIdAsync(id);

//            if (user == null)
//                return null;

//            return MapToDto(user);
//        }

//        public async Task<UserDto> CreateAsync(
//            CreateUserDto dto)
//        {
//            var email = dto.Email.Trim();

//            // Check duplicate email
//            var existingUser =
//                await _repository.GetByEmailAsync(email);

//            if (existingUser != null)
//            {
//                throw new InvalidOperationException(
//                    "A user with this email already exists.");
//            }

//            // Hash password
//            var passwordHash =
//                BCrypt.Net.BCrypt.HashPassword(
//                    dto.Password);

//            var user = new User
//            {
//                Email = email,
//                PasswordHash = passwordHash,
//                Role = dto.Role.Trim(),
//                IsActive = dto.IsActive,
//                CreatedAt = DateTime.UtcNow
//            };

//            await _repository.AddAsync(user);

//            await _repository.SaveChangesAsync();

//            // Reload user so generated Id is available
//            var createdUser =
//                await _repository.GetByIdAsync(user.Id);

//            return MapToDto(createdUser!);
//        }

//        public async Task<UserDto?> UpdateAsync(
//            int id,
//            UpdateUserDto dto)
//        {
//            var user =
//                await _repository.GetByIdAsync(id);

//            if (user == null)
//                return null;

//            var email = dto.Email.Trim();

//            // Check whether another user already uses email
//            var existingUser =
//                await _repository.GetByEmailAsync(email);

//            if (existingUser != null &&
//                existingUser.Id != id)
//            {
//                throw new InvalidOperationException(
//                    "A user with this email already exists.");
//            }

//            user.Email = email;
//            user.Role = dto.Role.Trim();
//            user.IsActive = dto.IsActive;

//            _repository.Update(user);

//            await _repository.SaveChangesAsync();

//            return MapToDto(user);
//        }

//        public async Task<bool> DeleteAsync(int id)
//        {
//            var user =
//                await _repository.GetByIdAsync(id);

//            if (user == null)
//                return false;

//            _repository.Delete(user);

//            await _repository.SaveChangesAsync();

//            return true;
//        }

//        private static UserDto MapToDto(User user)
//        {
//            return new UserDto
//            {
//                Id = user.Id,
//                Email = user.Email,
//                Role = user.Role,
//                IsActive = user.IsActive,
//                CreatedAt = user.CreatedAt
//            };
//        }
//    }
//}

using IMS.Api.DTOs.User;
using IMS.Api.Models;
using IMS.Api.Repositories.Roles;
using IMS.Api.Repositories.Users;

namespace IMS.Api.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IRoleRepository _roleRepository;

        public UserService(
            IUserRepository repository,
            IRoleRepository roleRepository)
        {
            _repository = repository;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<ListUserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            return users.Select(user => new ListUserDto
            {
                Id = user.Id,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role?.Name ?? string.Empty,
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

            var existingUser =
                await _repository.GetByEmailAsync(email);

            if (existingUser != null)
            {
                throw new InvalidOperationException(
                    "A user with this email already exists.");
            }

            var role =
                await _roleRepository.GetByIdAsync(dto.RoleId);

            if (role == null)
            {
                throw new InvalidOperationException(
                    "The selected role does not exist.");
            }

            if (!role.IsActive)
            {
                throw new InvalidOperationException(
                    "The selected role is inactive.");
            }

            var passwordHash =
                BCrypt.Net.BCrypt.HashPassword(
                    dto.Password);

            var user = new User
            {
                Email = email,
                PasswordHash = passwordHash,
                RoleId = dto.RoleId,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(user);

            await _repository.SaveChangesAsync();

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

            var existingUser =
                await _repository.GetByEmailAsync(email);

            if (existingUser != null &&
                existingUser.Id != id)
            {
                throw new InvalidOperationException(
                    "A user with this email already exists.");
            }

            var role =
                await _roleRepository.GetByIdAsync(dto.RoleId);

            if (role == null)
            {
                throw new InvalidOperationException(
                    "The selected role does not exist.");
            }

            if (!role.IsActive)
            {
                throw new InvalidOperationException(
                    "The selected role is inactive.");
            }

            user.Email = email;
            user.RoleId = dto.RoleId;
            user.IsActive = dto.IsActive;

            _repository.Update(user);

            await _repository.SaveChangesAsync();

            return MapToDto(user);
        }

        public async Task<UserDto?> AssignRoleAsync(
            int userId,
            AssignRoleDto dto)
        {
            var user =
                await _repository.GetByIdAsync(userId);

            if (user == null)
                return null;

            var role =
                await _roleRepository.GetByIdAsync(dto.RoleId);

            if (role == null)
            {
                throw new InvalidOperationException(
                    "The selected role does not exist.");
            }

            if (!role.IsActive)
            {
                throw new InvalidOperationException(
                    "The selected role is inactive.");
            }

            user.RoleId = dto.RoleId;

            _repository.Update(user);

            await _repository.SaveChangesAsync();

            var updatedUser =
                await _repository.GetByIdAsync(userId);

            return MapToDto(updatedUser!);
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
                RoleId = user.RoleId,
                RoleName = user.Role?.Name ?? string.Empty,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }
    }
}