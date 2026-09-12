using LMS.Domain.Entities;

public class EnrollmentServices(IEnrollmentRepository enrollmentRepository) : IEnrollmentServices
{
    public async Task<ServicesResponse<int>> GetActiveEnrollmentsCountAsync(int courseId)
    {
        var count = await enrollmentRepository.GetActiveEnrollmentsCountAsync(courseId);
        if (count == 0)
        {
            return new ServicesResponse<int>(false, "No active enrollments found for the given course id.", count);
        }
        return new ServicesResponse<int>(true, "Active enrollments found for the given course id.", count);
    }

    public async Task<ServicesResponse<IEnumerable<Enrollment>>> GetEnrollmentsByCourseAsync(int courseId)
    {
        var enrollments = await enrollmentRepository.GetEnrollmentsByCourseAsync(courseId);
        if (enrollments == null || !enrollments.Any())
        {
            return new ServicesResponse<IEnumerable<Enrollment>>(false, "No enrollments found for the given course id.", enrollments);
        }
        return new ServicesResponse<IEnumerable<Enrollment>>(true, "Enrollments found for the given course id.", enrollments);
    }

    public async Task<ServicesResponse<IEnumerable<Enrollment>>> GetEnrollmentsByStudentAsync(int studentId)
    {
        var enrollments = await enrollmentRepository.GetEnrollmentsByStudentAsync(studentId);
        if (enrollments == null || !enrollments.Any())
        {
            return new ServicesResponse<IEnumerable<Enrollment>>(false, "No enrollments found for the given student id.", enrollments);
        }
        return new ServicesResponse<IEnumerable<Enrollment>>(true, "Enrollments found for the given student id.", enrollments);
    }

    public async Task<ServicesResponse<bool>> IsEnrolledAsync(int studentId, int courseId)
    {
        var isEnrolled = await enrollmentRepository.IsEnrolledAsync(studentId, courseId);
        if (!isEnrolled)
        {
            return new ServicesResponse<bool>(false, "Enrollment not found for the given student id and course id.");
        }
        return new ServicesResponse<bool>(true, "Enrollment found for the given student id and course id.");
    }

    public async Task<ServicesResponse<bool>> MarkAsCompletedAsync(int enrollmentId)
    {
        await enrollmentRepository.MarkAsCompletedAsync(enrollmentId);
        return new ServicesResponse<bool>(true, "Enrollment marked as completed successfully.");
    }
}