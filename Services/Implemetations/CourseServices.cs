using LMS.Application.DTOs.Courses;
using LMS.Application.Mappers;
using LMS.Domain.Entities;

public class CourseServices(ICourseRepository courseRepository) : ICourseServices
{
    // بينشئ كورس جديد للمدرس الحالي، ويرجع تفاصيله كاملة بعد الإنشاء
    public async Task<ServicesResponse<CourseDetailsDto>> CreateCourseAsync(int instructorId, CourseCreateDto dto)
    {
        var course = dto.CourseCreateToEntityMapper(instructorId);
        await courseRepository.CreateAsync(course);

        var details = await courseRepository.GetCourseWithDetailsAsync(course.Id);
        if (details is null)
        {
            return new ServicesResponse<CourseDetailsDto>(false, "Course created but details could not be retrieved.");
        }

        return new ServicesResponse<CourseDetailsDto>(true, "Course created successfully.", details);
    }

    // بيعدّل الكورس، وبيتأكد الأول إن المدرس ده هو مالك الكورس
    public async Task<ServicesResponse<bool>> UpdateCourseAsync(int courseId, int instructorId, CourseUpdateDto dto)
    {
        var course = await courseRepository.GetByIdAsync(courseId);
        if (course is null)
        {
            return new ServicesResponse<bool>(false, "Course not found.");
        }

        if (course.InstructorId != instructorId)
        {
            return new ServicesResponse<bool>(false, "You do not own this course.");
        }

        course.CourseUpdateMapper(dto);
        await courseRepository.UpdateAsync(course);

        return new ServicesResponse<bool>(true, "Course updated successfully.", true);
    }

    // بيحذف الكورس، بنفس تأكيد الملكية
    public async Task<ServicesResponse<bool>> DeleteCourseAsync(int courseId, int instructorId)
    {
        var course = await courseRepository.GetByIdAsync(courseId);
        if (course is null)
        {
            return new ServicesResponse<bool>(false, "Course not found.");
        }

        if (course.InstructorId != instructorId)
        {
            return new ServicesResponse<bool>(false, "You do not own this course.");
        }

        await courseRepository.DeleteAsync(courseId);
        return new ServicesResponse<bool>(true, "Course deleted successfully.", true);
    }


    public async Task<ServicesResponse<IEnumerable<Course>>> GetCoursesByInstructorAsync(int instructorId)
    {
        var courses = await courseRepository.GetCoursesByInstructorAsync(instructorId);
        if (courses == null || !courses.Any())
        {
            return new ServicesResponse<IEnumerable<Course>>(false, "No courses found for the instructor.");
        }
        return new ServicesResponse<IEnumerable<Course>>(true, "Courses found for the instructor.", courses);
    }

    public async Task<ServicesResponse<CourseDetailsDto?>> GetCourseWithDetailsAsync(int courseId)
    {
        var course = await courseRepository.GetCourseWithDetailsAsync(courseId);
        if (course == null)
        {
            return new ServicesResponse<CourseDetailsDto?>(false, "No course found for the given id.");
        }
        return new ServicesResponse<CourseDetailsDto?>(true, "Course found for the given id.", course);
    }

    public async Task<ServicesResponse<int>> GetPublishedCoursesCountAsync()
    {
        var count = await courseRepository.GetPublishedCoursesCountAsync();
        if (count == 0)
        {
            return new ServicesResponse<int>(false, "No courses found.", count);
        }
        return new ServicesResponse<int>(true, "Courses found.", count);
    }

    public async Task<ServicesResponse<IEnumerable<Course>>> GetTopSellingCoursesAsync(int count)
    {
        var courses = await courseRepository.GetTopSellingCoursesAsync(count);
        if (courses == null || !courses.Any())
        {
            return new ServicesResponse<IEnumerable<Course>>(false, "No courses found.", courses);
        }
        return new ServicesResponse<IEnumerable<Course>>(true, "Courses found.", courses);
    }

    public async Task<ServicesResponse<bool>> IsOwnedByInstructorAsync(int courseId, int instructorId)
    {
        var course = await courseRepository.GetByIdAsync(courseId);
        if (course == null)
        {
            return new ServicesResponse<bool>(false, "No course found for the given id.");
        }
        var isOwned = await courseRepository.IsOwnedByInstructorAsync(courseId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<bool>(false, "Ownership status not found.");
        }
        return new ServicesResponse<bool>(true, "Ownership status retrieved.", isOwned);
    }
}