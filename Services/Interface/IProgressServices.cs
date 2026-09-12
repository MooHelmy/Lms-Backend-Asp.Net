using LMS.Domain.Entities;

public interface IProgressServices
{
    Task<ServicesResponse<LessonProgress?>> GetProgressAsync(int studentId, int lessonId);
    Task<ServicesResponse<int>> GetCompletedLessonsCountAsync(int studentId, int courseId);
    Task<ServicesResponse<bool>> MarkLessonCompletedAsync(int studentId, int lessonId);
}
