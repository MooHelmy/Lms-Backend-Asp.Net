using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class CourseRepository(DbContext context) : GenericRepository<Course>(context), ICourseRepository
{


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