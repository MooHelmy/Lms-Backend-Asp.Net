using LMS.Domain.Entities;

public interface ILessonServices
{
    Task<ServicesResponse<IEnumerable<Lesson>>> GetLessonsBySectionAsync(int sectionId);
    Task<ServicesResponse<Lesson?>> GetLessonWithSectionAsync(int lessonId);
    Task<ServicesResponse> GetTotalDurationByCourseAsync(int courseId);
    Task<ServicesResponse> CountLessonsByCourseAsync(int courseId);
}
