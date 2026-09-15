using LMS.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/users")]
[Authorize]
public class UserController(IUserServices userServices) : BaseApiController
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await userServices.RegisterAsync(dto);
        return HandleResponse(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await userServices.LoginAsync(dto);
        return HandleResponse(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
    {
        var result = await userServices.RefreshTokenAsync(dto);
        return HandleResponse(result);
    }
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        var result = await userServices.ForgotPasswordAsync(dto);
        return HandleResponse(result);
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
    {
        var result = await userServices.ResetPasswordAsync(dto);
        return HandleResponse(result);
    }
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var result = await userServices.ConfirmEmailAsync(userId, token);
        return HandleResponse(result);
    }
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(string userId, ChangePasswordDto dto)
    {
        var result = await userServices.ChangePasswordAsync(userId, dto);
        return HandleResponse(result);
    }
    [HttpGet("Users/{userId}")]
    public async Task<IActionResult> GetProfile(string userId)
    {
        var result = await userServices.GetProfileAsync(userId);
        return HandleResponse(result);
    }
    [HttpGet("Users")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetallUsers()
    {
        var result = await userServices.GetAllUsersAsync();
        return HandleResponse(result);
    }
    [HttpPut("Users/{userId}")]

    public async Task<IActionResult> UpdateProfile(string userId, UpdateProfileDto dto)
    {
        var result = await userServices.UpdateProfileAsync(userId, dto);
        return HandleResponse(result);
    }
    [HttpPut("Users/{userId}/roles/{roleName}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignRole(string userId, string roleName)
    {
        var result = await userServices.AssignRoleAsync(userId, roleName);
        return HandleResponse(result);
    }
    [HttpDelete("Users/{userId}/roles/{roleName}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveRole(string userId, string roleName)
    {
        var result = await userServices.RemoveRoleAsync(userId, roleName);
        return HandleResponse(result);
    }
    [HttpPatch("{userId:int}/deactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deactivate(String userId)
    {
        var result = await userServices.DeactivateAsync(userId);
        return HandleResponse(result);
    }
    [AllowAnonymous]
    [HttpGet("email-exists/{email}")]
    public async Task<IActionResult> EmailExists(string email)
    {
        var result = await userServices.EmailExistsAsync(email);
        return HandleResponse(result);
    }
    [HttpGet("email/{email}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await userServices.GetByEmailAsync(email);
        return HandleResponse(result);
    }
    [HttpGet("instructors")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetInstructors()
    {
        var result = await userServices.GetInstructorsAsync();
        return HandleResponse(result);
    }

    [HttpGet("students")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetStudents()
    {
        var result = await userServices.GetStudentsAsync();
        return HandleResponse(result);
    }
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(string userId)
    {
        var result = await userServices.LogoutAsync(userId);
        return HandleResponse(result);
    }

}