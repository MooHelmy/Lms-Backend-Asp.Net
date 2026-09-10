// بيرث كل ميثودز IGeneric<Course> العامة (GetAllAsync, GetByIdAsync, CreateAsync, UpdateAsync, DeleteAsync)
// وبيضيف بس اللي خاص بالكورس تحديدًا.


using LMS.Domain.Entities;

public interface ICourseRepository : IGeneric<Course>
{
    Task<IEnumerable<Course>> GetTopSellingCoursesAsync(int count);
    Task<bool> IsOwnedByInstructorAsync(int courseId, int instructorId);
}
