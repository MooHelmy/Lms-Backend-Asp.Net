using LMS.Domain.Entities;

public interface IProgressRepository : IGeneric<LessonProgress>
{
    Task<LessonProgress?> GetProgressAsync(String studentId, int lessonId);
    Task<int> GetCompletedLessonsCountAsync(String studentId, int courseId);
    Task MarkLessonCompletedAsync(String studentId, int lessonId);
}
