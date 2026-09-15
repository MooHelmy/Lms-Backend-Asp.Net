using LMS.Application.DTOs.Progress;
using LMS.Domain.Entities;

public interface IProgressServices
{
    Task<ServicesResponse<LessonProgress?>> GetProgressAsync(String studentId, int lessonId);
    Task<ServicesResponse<int>> GetCompletedLessonsCountAsync(String studentId, int courseId);
    Task<ServicesResponse<bool>> MarkLessonCompletedAsync(String studentId, int lessonId);

    Task<ServicesResponse<bool>> UpdateLessonProgressAsync(String studentId, UpdateLessonProgressDto dto);
    Task<ServicesResponse<CourseProgressDto>> GetCourseProgressAsync(String studentId, int courseId);
}