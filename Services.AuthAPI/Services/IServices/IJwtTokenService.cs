using Services.AuthAPI.Models;

namespace Services.AuthAPI.Services.IServices
{
    public interface IJwtTokenService
    {
        string GenerateToken (ApplicationUser user, IEnumerable<string> roles);
    }
}
