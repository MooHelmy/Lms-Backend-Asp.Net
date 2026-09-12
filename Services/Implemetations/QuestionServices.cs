using LMS.Domain.Entities;

public class QuestionServices(IQuestionRepository questionRepository) : IQuestionServices
{
    public async Task<ServicesResponse<Answer?>> GetCorrectAnswerAsync(int questionId)
    {
        var answer = await questionRepository.GetCorrectAnswerAsync(questionId);
        if (answer == null)
        {
            return new ServicesResponse<Answer?>(false, "Answer not found.", null);
        }
        return new ServicesResponse<Answer?>(true, "Answer found.", answer);
    }

    public async Task<ServicesResponse<IEnumerable<Question>>> GetQuestionsByExamAsync(int examId)
    {
        var questions = await questionRepository.GetQuestionsByExamAsync(examId);
        if (questions == null || !questions.Any())
        {
            return new ServicesResponse<IEnumerable<Question>>(false, "No questions found for the specified exam.", null);
        }
        return new ServicesResponse<IEnumerable<Question>>(true, "Questions found for the specified exam.", questions);
    }

    public async Task<ServicesResponse<int>> GetTotalPointsAsync(int examId)
    {
        var totalPoints = await questionRepository.GetTotalPointsAsync(examId);
        return new ServicesResponse<int>(true, "Total points calculated.", totalPoints);
    }
}