using LMS.Application.DTOs.ExamAttempts;
using LMS.Domain.Entities;

public interface IExamAttemptServices
{
    Task<ServicesResponse<IEnumerable<ExamAttempt>>> GetAttemptsByStudentAsync(int studentId, int examId);
    Task<ServicesResponse<ExamAttempt?>> GetAttemptWithAnswersAsync(int attemptId);
    Task<ServicesResponse<int>> GetAttemptsCountAsync(int studentId, int examId);
    Task<ServicesResponse<ExamAttempt?>> GetLatestAttemptAsync(int studentId, int examId);
    Task<ServicesResponse<bool>> HasPassedAsync(int studentId, int examId);

    Task<ServicesResponse<ExamAttemptResultDto>> StartAttemptAsync(int studentId, StartExamAttemptDto dto);
    Task<ServicesResponse<ExamAttemptResultDto>> SubmitAttemptAsync(int studentId, SubmitExamAttemptDto dto);
}