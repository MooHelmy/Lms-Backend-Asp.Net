using LMS.Application.DTOs.ExamAttempts;
using LMS.Domain.Entities;

public interface IExamAttemptServices
{
    Task<ServicesResponse<IEnumerable<ExamAttempt>>> GetAttemptsByStudentAsync(String studentId, int examId);
    Task<ServicesResponse<ExamAttempt?>> GetAttemptWithAnswersAsync(int attemptId);
    Task<ServicesResponse<int>> GetAttemptsCountAsync(String studentId, int examId);
    Task<ServicesResponse<ExamAttempt?>> GetLatestAttemptAsync(String studentId, int examId);
    Task<ServicesResponse<bool>> HasPassedAsync(String studentId, int examId);

    Task<ServicesResponse<ExamAttemptResultDto>> StartAttemptAsync(String studentId, StartExamAttemptDto dto);
    Task<ServicesResponse<ExamAttemptResultDto>> SubmitAttemptAsync(String studentId, SubmitExamAttemptDto dto);
}