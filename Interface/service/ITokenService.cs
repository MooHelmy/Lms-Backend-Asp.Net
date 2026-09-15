using LMS.Domain.Entities;

public interface ITokenService
{
    string CreateToken(User user, IList<string> roles);
    string GenerateRefreshToken();
}