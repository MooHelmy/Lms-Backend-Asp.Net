using LMS.Domain.Entities;

public interface IExamServices
{
    Task<ServicesResponse<Exam?>> GetExamWithQuestionsAsync(int examId);
    Task<ServicesResponse<IEnumerable<Exam>>> GetExamsByCourseAsync(int courseId);
    Task<ServicesResponse> IsOwnedByInstructorAsync(int examId, int instructorId);
}
