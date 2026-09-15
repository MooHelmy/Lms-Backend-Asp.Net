using LMS.Domain.Entities;

public interface IEnrollmentRepository : IGeneric<Enrollment>
{
    Task<bool> IsEnrolledAsync(String studentId, int courseId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(String studentId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseAsync(int courseId);
    Task<int> GetActiveEnrollmentsCountAsync(int courseId);
    Task MarkAsCompletedAsync(int enrollmentId);
}
