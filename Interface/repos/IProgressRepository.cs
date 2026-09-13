using LMS.Domain.Entities;

public interface IProgressRepository : IGeneric<LessonProgress>
{
    Task<LessonProgress?> GetProgressAsync(int studentId, int lessonId);
    Task<int> GetCompletedLessonsCountAsync(int studentId, int courseId);
    Task MarkLessonCompletedAsync(int studentId, int lessonId);
}
