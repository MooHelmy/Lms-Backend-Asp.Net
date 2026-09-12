using LMS.Domain.Entities;

public interface IEnrollmentServices
{
    Task<ServicesResponse> IsEnrolledAsync(int studentId, int courseId);
    Task<ServicesResponse<IEnumerable<Enrollment>>> GetEnrollmentsByStudentAsync(int studentId);
    Task<ServicesResponse<IEnumerable<Enrollment>>> GetEnrollmentsByCourseAsync(int courseId);
    Task<ServicesResponse> GetActiveEnrollmentsCountAsync(int courseId);
    Task<ServicesResponse> MarkAsCompletedAsync(int enrollmentId);
}
