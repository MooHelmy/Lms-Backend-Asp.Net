using LMS.Domain.Entities;

public class ExamAttemptServices(IExamAttemptRepository examAttemptRepository) : IExamAttemptServices
{
    public async Task<ServicesResponse<IEnumerable<ExamAttempt>>> GetAttemptsByStudentAsync(int studentId, int examId)
    {
        var attempts = await examAttemptRepository.GetAttemptsByStudentAsync(studentId, examId);
        if (attempts == null || !attempts.Any())
        {
            return new ServicesResponse<IEnumerable<ExamAttempt>>(false, "No attempts found for the given student id and exam id.", attempts);
        }
        return new ServicesResponse<IEnumerable<ExamAttempt>>(true, "Attempts found for the given student id and exam id.", attempts);
    }

    public async Task<ServicesResponse<int>> GetAttemptsCountAsync(int studentId, int examId)
    {
        var count = await examAttemptRepository.GetAttemptsCountAsync(studentId, examId);
        if (count == 0)
        {
            return new ServicesResponse<int>(false, "No attempts found for the given student id and exam id.", count);
        }
        return new ServicesResponse<int>(true, "Attempt count found for the given student id and exam id.", count);
    }

    public async Task<ServicesResponse<ExamAttempt?>> GetAttemptWithAnswersAsync(int attemptId)
    {
        var attempt = await examAttemptRepository.GetAttemptWithAnswersAsync(attemptId);
        if (attempt == null)
        {
            return new ServicesResponse<ExamAttempt?>(false, "No attempt found for the given attempt id.", attempt);
        }
        return new ServicesResponse<ExamAttempt?>(true, "Attempt found for the given attempt id.", attempt);
    }

    public async Task<ServicesResponse<ExamAttempt?>> GetLatestAttemptAsync(int studentId, int examId)
    {
        var attempt = await examAttemptRepository.GetLatestAttemptAsync(studentId, examId);
        if (attempt == null)
        {
            return new ServicesResponse<ExamAttempt?>(false, "No attempt found for the given student id and exam id.", attempt);
        }
        return new ServicesResponse<ExamAttempt?>(true, "Attempt found for the given student id and exam id.", attempt);
    }

    public async Task<ServicesResponse<bool>> HasPassedAsync(int studentId, int examId)
    {
        var passed = await examAttemptRepository.HasPassedAsync(studentId, examId);
        if (!passed)
        {
            return new ServicesResponse<bool>(false, "No attempt found for the given student id and exam id.");
        }
        return new ServicesResponse<bool>(true, "Attempt found for the given student id and exam id.");
    }
}