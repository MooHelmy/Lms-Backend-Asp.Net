using LMS.Application.DTOs.Exams;
using LMS.Domain.Entities;

public interface IExamServices
{
    Task<ServicesResponse<Exam?>> GetExamWithQuestionsAsync(int examId);
    Task<ServicesResponse<IEnumerable<Exam>>> GetExamsByCourseAsync(int courseId);
    Task<ServicesResponse> IsOwnedByInstructorAsync(int examId, String instructorId);

    Task<ServicesResponse<Exam>> CreateExamAsync(String instructorId, ExamCreateDto dto);
    Task<ServicesResponse<bool>> UpdateExamAsync(int examId, String instructorId, ExamUpdateDto dto);
    Task<ServicesResponse<bool>> DeleteExamAsync(int examId, String instructorId);
}