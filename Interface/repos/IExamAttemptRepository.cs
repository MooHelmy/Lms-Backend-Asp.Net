using LMS.Domain.Entities;

public interface IExamAttemptRepository : IGeneric<ExamAttempt>
{
    Task<IEnumerable<ExamAttempt>> GetAttemptsByStudentAsync(String studentId, int examId);
    Task<ExamAttempt?> GetAttemptWithAnswersAsync(int attemptId);
    Task<int> GetAttemptsCountAsync(String studentId, int examId);
    Task<ExamAttempt?> GetLatestAttemptAsync(String studentId, int examId);
    Task<bool> HasPassedAsync(String studentId, int examId);
}
