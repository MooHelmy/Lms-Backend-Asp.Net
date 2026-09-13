using LMS.Application.DTOs.Exams;
using LMS.Application.Mappers;
using LMS.Domain.Entities;

// محتاجين IExamRepository عشان نتأكد إن المدرس مالك الامتحان قبل ما يضيف/يحذف سؤال منه
public class QuestionServices(IQuestionRepository questionRepository, IExamRepository examRepository) : IQuestionServices
{
    // بيضيف سؤال جديد (مع إجاباته) للامتحان، بعد التأكد من الملكية
    public async Task<ServicesResponse<Question>> AddQuestionAsync(int examId, int instructorId, QuestionCreateDto dto)
    {
        var isOwned = await examRepository.IsOwnedByInstructorAsync(examId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<Question>(false, "You do not own this course.");
        }

        var question = dto.QuestionCreateToEntityMapper(examId);
        await questionRepository.CreateAsync(question);

        return new ServicesResponse<Question>(true, "Question added successfully.", question);
    }

    // بيحذف السؤال، بعد التأكد إن الامتحان اللي السؤال ده تابعله ملك المدرس
    public async Task<ServicesResponse<bool>> DeleteQuestionAsync(int questionId, int instructorId)
    {
        var question = await questionRepository.GetByIdAsync(questionId);
        if (question is null)
        {
            return new ServicesResponse<bool>(false, "Question not found.");
        }

        var isOwned = await examRepository.IsOwnedByInstructorAsync(question.ExamId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<bool>(false, "You do not own this course.");
        }

        await questionRepository.DeleteAsync(questionId);
        return new ServicesResponse<bool>(true, "Question deleted successfully.", true);
    }


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