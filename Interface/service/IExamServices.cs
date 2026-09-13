using LMS.Application.DTOs.Exams;
using LMS.Domain.Entities;

public interface IExamServices
{
    Task<ServicesResponse<Exam?>> GetExamWithQuestionsAsync(int examId);
    Task<ServicesResponse<IEnumerable<Exam>>> GetExamsByCourseAsync(int courseId);
    Task<ServicesResponse> IsOwnedByInstructorAsync(int examId, int instructorId);

    Task<ServicesResponse<Exam>> CreateExamAsync(int instructorId, ExamCreateDto dto);
    Task<ServicesResponse<bool>> UpdateExamAsync(int examId, int instructorId, ExamUpdateDto dto);
    Task<ServicesResponse<bool>> DeleteExamAsync(int examId, int instructorId);
}