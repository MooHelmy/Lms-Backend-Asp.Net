using LMS.Application.DTOs.Enrollments;
using LMS.Domain.Entities;

public interface IEnrollmentServices
{
    Task<ServicesResponse<bool>> IsEnrolledAsync(String studentId, int courseId);
    Task<ServicesResponse<IEnumerable<Enrollment>>> GetEnrollmentsByStudentAsync(String studentId);
    Task<ServicesResponse<IEnumerable<Enrollment>>> GetEnrollmentsByCourseAsync(int courseId);
    Task<ServicesResponse<int>> GetActiveEnrollmentsCountAsync(int courseId);
    Task<ServicesResponse<bool>> MarkAsCompletedAsync(int enrollmentId);

    Task<ServicesResponse<EnrollmentResponseDto>> EnrollAsync(String studentId, EnrollDto dto);
    Task<ServicesResponse<bool>> CancelEnrollmentAsync(String studentId, int courseId);
}