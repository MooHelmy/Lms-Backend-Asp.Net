using LMS.Domain.Entities;

public interface IEnrollmentServices
{
    Task<ServicesResponse<bool>> IsEnrolledAsync(int studentId, int courseId);
    Task<ServicesResponse<IEnumerable<Enrollment>>> GetEnrollmentsByStudentAsync(int studentId);
    Task<ServicesResponse<IEnumerable<Enrollment>>> GetEnrollmentsByCourseAsync(int courseId);
    Task<ServicesResponse<int>> GetActiveEnrollmentsCountAsync(int courseId);
    Task<ServicesResponse<bool>> MarkAsCompletedAsync(int enrollmentId);
}
