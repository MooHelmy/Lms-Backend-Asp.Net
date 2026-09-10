using LMS.Domain.Entities;

public interface IExamRepository : IGeneric<Exam>
{
    Task<Exam?> GetExamWithQuestionsAsync(int examId);
    Task<IEnumerable<Exam>> GetExamsByCourseAsync(int courseId);
    Task<bool> IsOwnedByInstructorAsync(int examId, int instructorId);
}
