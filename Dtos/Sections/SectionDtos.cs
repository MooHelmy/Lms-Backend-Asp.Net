namespace LMS.Application.DTOs.Sections
{
    public class SectionCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Order { get; set; }
        public int CourseId { get; set; }
    }

    public class SectionUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Order { get; set; }
    }

    public class SectionResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Order { get; set; }
        public int CourseId { get; set; }
    }
}
