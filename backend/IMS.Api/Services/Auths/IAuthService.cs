using IMS.Api.DTOs.Auth;

namespace IMS.Api.Services.Auths
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}