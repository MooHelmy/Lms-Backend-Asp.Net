using LMS.Domain.Entities;

public interface IExamAttemptServices
{
    Task<ServicesResponse<IEnumerable<ExamAttempt>>> GetAttemptsByStudentAsync(int studentId, int examId);
    Task<ServicesResponse<ExamAttempt?>> GetAttemptWithAnswersAsync(int attemptId);
    Task<ServicesResponse> GetAttemptsCountAsync(int studentId, int examId);
    Task<ServicesResponse<ExamAttempt?>> GetLatestAttemptAsync(int studentId, int examId);
    Task<ServicesResponse> HasPassedAsync(int studentId, int examId);
}
