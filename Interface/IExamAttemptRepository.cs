using LMS.Domain.Entities;

public interface IExamAttemptRepository : IGeneric<ExamAttempt>
{
    Task<IEnumerable<ExamAttempt>> GetAttemptsByStudentAsync(int studentId, int examId);
    Task<ExamAttempt?> GetAttemptWithAnswersAsync(int attemptId);
    Task<int> GetAttemptsCountAsync(int studentId, int examId);
    Task<ExamAttempt?> GetLatestAttemptAsync(int studentId, int examId);
    Task<bool> HasPassedAsync(int studentId, int examId);
}
