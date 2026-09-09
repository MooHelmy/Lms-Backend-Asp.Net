namespace LMS.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ThumbnailUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsPublished { get; set; }

        public int InstructorId { get; set; }
        public User Instructor { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<Section> Sections { get; set; } = new List<Section>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}
