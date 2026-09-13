using LMS.Application.DTOs.Exams;
using LMS.Domain.Entities;

public interface IQuestionServices
{
    Task<ServicesResponse<IEnumerable<Question>>> GetQuestionsByExamAsync(int examId);
    Task<ServicesResponse<int>> GetTotalPointsAsync(int examId);
    Task<ServicesResponse<Answer?>> GetCorrectAnswerAsync(int questionId);

    Task<ServicesResponse<Question>> AddQuestionAsync(int examId, int instructorId, QuestionCreateDto dto);
    Task<ServicesResponse<bool>> DeleteQuestionAsync(int questionId, int instructorId);
}