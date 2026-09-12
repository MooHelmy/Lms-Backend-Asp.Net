using LMS.Domain.Entities;

public interface IQuestionServices
{
    Task<ServicesResponse<IEnumerable<Question>>> GetQuestionsByExamAsync(int examId);
    Task<ServicesResponse> GetTotalPointsAsync(int examId);
    Task<ServicesResponse<Answer?>> GetCorrectAnswerAsync(int questionId);
}
