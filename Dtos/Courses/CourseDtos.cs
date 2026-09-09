namespace LMS.Application.DTOs.Courses
{
    public class CourseCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }

    public class CourseUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsPublished { get; set; }
    }

    public class CourseListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ThumbnailUrl { get; set; }
        public decimal Price { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public int ReviewsCount { get; set; }
        public int EnrollmentsCount { get; set; }
    }

    public class CourseDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ThumbnailUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsPublished { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public int ReviewsCount { get; set; }
        public int TotalLessons { get; set; }
        public int TotalDurationInSeconds { get; set; }
        public List<SectionSummaryDto> Sections { get; set; } = new();
    }

    public class SectionSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
        public List<LessonSummaryDto> Lessons { get; set; } = new();
    }

    public class LessonSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public int DurationInSeconds { get; set; }
        public int Order { get; set; }
        public bool IsCompleted { get; set; } // للطالب الحالي لو مسجل
    }

    public class CourseFilterDto : LMS.Application.DTOs.Common.PaginationParams
    {
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? InstructorId { get; set; }
    }
}
