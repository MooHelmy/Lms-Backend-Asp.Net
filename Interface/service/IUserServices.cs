using System.Security.Claims;
using LMS.Domain.Entities;

public interface IUserServices
{
    Task<ServicesResponse<AuthResponse>> RegisterAsync(RegisterDto dto);
    Task<ServicesResponse<AuthResponse>> LoginAsync(LoginDto dto);
    Task<ServicesResponse> LogoutAsync(string userId);
    Task<ServicesResponse<AuthResponse>> RefreshTokenAsync(RefreshTokenDto dto);
    Task<ServicesResponse> ForgotPasswordAsync(ForgotPasswordDto dto);
    Task<ServicesResponse> ResetPasswordAsync(ResetPasswordDto dto);
    Task<ServicesResponse> ConfirmEmailAsync(string userId, string token);
    Task<ServicesResponse> ChangePasswordAsync(string userId, ChangePasswordDto dto);
    Task<ServicesResponse<IEnumerable<GetProfile>>> GetAllUsersAsync();
    Task<ServicesResponse<GetProfile>> GetProfileAsync(string userId);
    Task<ServicesResponse> UpdateProfileAsync(string userId, UpdateProfileDto dto);
    Task<ServicesResponse> AssignRoleAsync(string userId, string roleName);
    Task<ServicesResponse> RemoveRoleAsync(string userId, string roleName);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);


    Task<ServicesResponse<User?>> GetByEmailAsync(string email);
    Task<ServicesResponse<bool>> EmailExistsAsync(string email);
    Task<ServicesResponse<IEnumerable<User>>> GetInstructorsAsync();
    Task<ServicesResponse<IEnumerable<User>>> GetStudentsAsync();
    Task<ServicesResponse<bool>> DeactivateAsync(int userId);
}