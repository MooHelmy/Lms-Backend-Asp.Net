using System.Linq.Expressions;
using LMS.Application.DTOs.Courses;
using LMS.Domain.Entities;

public class CourseServices : ICourseRepository
{
    public Task<int> CountAsync(Expression<Func<Course, bool>>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateAsync(Course entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<Course, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Course>> FindAsync(Expression<Func<Course, bool>> predicate, params Expression<Func<Course, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Course>> GetAllAsync(params Expression<Func<Course, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<Course?> GetByIdAsync(int id, params Expression<Func<Course, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Course>> GetCoursesByInstructorAsync(int instructorId)
    {
        throw new NotImplementedException();
    }

    public Task<CourseDetailsDto?> GetCourseWithDetailsAsync(int courseId)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetPublishedCoursesCountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Course>> GetTopSellingCoursesAsync(int count)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsOwnedByInstructorAsync(int courseId, int instructorId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Course> Query()
    {
        throw new NotImplementedException();
    }

    public Task<Course?> SingleOrDefaultAsync(Expression<Func<Course, bool>> predicate, params Expression<Func<Course, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Course entity)
    {
        throw new NotImplementedException();
    }
}