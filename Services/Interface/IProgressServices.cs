using LMS.Domain.Entities;

public interface IProgressServices
{
    Task<ServicesResponse<LessonProgress?>> GetProgressAsync(int studentId, int lessonId);
    Task<ServicesResponse> GetCompletedLessonsCountAsync(int studentId, int courseId);
    Task<ServicesResponse> MarkLessonCompletedAsync(int studentId, int lessonId);
}
