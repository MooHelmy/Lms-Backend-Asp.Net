using LMS.Application.DTOs.Courses;
using LMS.Application.Mappers;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class CourseRepository(DbContext context) : GenericRepository<Course>(context), ICourseRepository
{
    public async Task<IEnumerable<Course>> GetCoursesByInstructorAsync(int instructorId)
    {
        return await dbSet.Where(c => c.InstructorId == instructorId).ToListAsync();
    }

    public async Task<CourseDetailsDto?> GetCourseWithDetailsAsync(int courseId)
    {
        var course = await dbSet.FindAsync(courseId);

        if (course is null)
        {
            return null;
        }

        return course.CourseToDetailsMapper();
    }

    public async Task<int> GetPublishedCoursesCountAsync()
    {
        return await dbSet.CountAsync(c => c.IsPublished);
    }

    public async Task<IEnumerable<Course>> GetTopSellingCoursesAsync(int count)
    {
        return await dbSet
           .OrderByDescending(c => c.Enrollments.Count)
           .Take(count)
           .ToListAsync();
    }

    public async Task<bool> IsOwnedByInstructorAsync(int courseId, int instructorId)
    {
        return await dbSet.AnyAsync(c => c.Id == courseId && c.InstructorId == instructorId);
    }
}