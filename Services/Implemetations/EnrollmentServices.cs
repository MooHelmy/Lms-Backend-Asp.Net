using LMS.Application.DTOs.Enrollments;
using LMS.Application.Mappers;
using LMS.Domain.Entities;

// محتاجين ICourseRepository عشان نتأكد إن الكورس موجود أصلًا قبل تسجيل الطالب فيه
public class EnrollmentServices(IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository) : IEnrollmentServices
{
    // بيسجل الطالب في كورس، بعد التأكد إن الكورس موجود وإنه مش مسجل فيه أصلًا
    public async Task<ServicesResponse<EnrollmentResponseDto>> EnrollAsync(String studentId, EnrollDto dto)
    {
        var course = await courseRepository.GetByIdAsync(dto.CourseId);
        if (course is null)
        {
            return new ServicesResponse<EnrollmentResponseDto>(false, "Course not found.");
        }

        var alreadyEnrolled = await enrollmentRepository.IsEnrolledAsync(studentId, dto.CourseId);
        if (alreadyEnrolled)
        {
            return new ServicesResponse<EnrollmentResponseDto>(false, "Already enrolled in this course.");
        }

        var enrollment = dto.EnrollmentCreateToEntityMapper(studentId);
        await enrollmentRepository.CreateAsync(enrollment);
        enrollment.Course = course;

        return new ServicesResponse<EnrollmentResponseDto>(true, "Enrolled successfully.", enrollment.EnrollmentToResponseMapper());
    }

    // بيلغي تسجيل الطالب في كورس معين (بيغيّر الحالة لـ Cancelled بدل الحذف الفعلي)
    public async Task<ServicesResponse<bool>> CancelEnrollmentAsync(String studentId, int courseId)
    {
        var enrollment = await enrollmentRepository.SingleOrDefaultAsync(
            e => e.StudentId == studentId && e.CourseId == courseId);

        if (enrollment is null)
        {
            return new ServicesResponse<bool>(false, "Enrollment not found.");
        }

        enrollment.Status = EnrollmentStatus.Cancelled;
        await enrollmentRepository.UpdateAsync(enrollment);

        return new ServicesResponse<bool>(true, "Enrollment cancelled successfully.", true);
    }


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

    public async Task<ServicesResponse<IEnumerable<Enrollment>>> GetEnrollmentsByStudentAsync(String studentId)
    {
        var enrollments = await enrollmentRepository.GetEnrollmentsByStudentAsync(studentId);
        if (enrollments == null || !enrollments.Any())
        {
            return new ServicesResponse<IEnumerable<Enrollment>>(false, "No enrollments found for the given student id.", enrollments);
        }
        return new ServicesResponse<IEnumerable<Enrollment>>(true, "Enrollments found for the given student id.", enrollments);
    }

    public async Task<ServicesResponse<bool>> IsEnrolledAsync(String studentId, int courseId)
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