using IMS.Api.DTOs.Auth;
using IMS.Api.Repositories.Auths;

namespace IMS.Api.Services.Auths
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IJwtService _jwtService;

        public AuthService(
            IAuthRepository repository,
            IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var email = dto.Email.Trim();

            var user = await _repository.GetByEmailAsync(email);

            // User does not exist
            if (user == null)
            {
                return null;
            }

            // User is inactive
            if (!user.IsActive)
            {
                return null;
            }

            // Verify password
            var passwordValid = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash);

            if (!passwordValid)
            {
                return null;
            }

            // Generate JWT
            var token = _jwtService.GenerateToken(
                user.Id,
                user.Role.Name);

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role.Name
            };
        }
    }
}