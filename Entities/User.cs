using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LMS.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string StreetName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public UserRole Role { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Course> CoursesCreated { get; set; } = new List<Course>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}
