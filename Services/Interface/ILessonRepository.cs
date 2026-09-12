using LMS.Domain.Entities;

public interface ILessonServices
{
    Task<ServicesResponse<IEnumerable<Lesson>>> GetLessonsBySectionAsync(int sectionId);
    Task<ServicesResponse<Lesson?>> GetLessonWithSectionAsync(int lessonId);
    Task<ServicesResponse<double>> GetTotalDurationByCourseAsync(int courseId);
    Task<ServicesResponse<int>> CountLessonsByCourseAsync(int courseId);
}
