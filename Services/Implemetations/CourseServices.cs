using LMS.Application.DTOs.Courses;
using LMS.Domain.Entities;

public class CourseServices(ICourseRepository courseRepository) : ICourseServices
{

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