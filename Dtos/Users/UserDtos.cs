using LMS.Domain.Entities;

namespace LMS.Application.DTOs.Users
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UpdateProfileDto
    {
        public string Name { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
    }

    public class AdminUpdateUserDto
    {
        public bool IsActive { get; set; }
        public UserRole Role { get; set; }
    }
}
