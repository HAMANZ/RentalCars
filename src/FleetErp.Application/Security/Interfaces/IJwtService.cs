using FleetErp.Domain.Entities.Security;

namespace FleetErp.Application.Security.Interfaces;

public interface IJwtService
{
    (string Token, DateTime ExpiresAt) GenerateAccessToken(User user, IEnumerable<string> permissions);
    string GenerateRefreshToken();
    int? ValidateAccessToken(string token);
}
