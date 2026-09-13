using LMS.Domain.Entities;

public interface IQuestionRepository : IGeneric<Question>
{
    Task<IEnumerable<Question>> GetQuestionsByExamAsync(int examId);
    Task<int> GetTotalPointsAsync(int examId);
    Task<Answer?> GetCorrectAnswerAsync(int questionId);
}
