namespace IMS.Api.Services.Auths
{
    public interface IJwtService
    {
        string GenerateToken(
            int userId,
            string role);
    }
}