using LMS.Domain.Entities;

public interface ILessonRepository : IGeneric<Lesson>
{
    Task<IEnumerable<Lesson>> GetLessonsBySectionAsync(int sectionId);
    Task<Lesson?> GetLessonWithSectionAsync(int lessonId);
    Task<int> GetTotalDurationByCourseAsync(int courseId);
    Task<int> CountLessonsByCourseAsync(int courseId);
}
