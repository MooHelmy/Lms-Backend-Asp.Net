using LMS.Application.DTOs.Common;
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
    // بيرجع كل الكورسات المنشورة مع Search + فلترة (تصنيف/سعر/مدرس) + ترتيب + Pagination
    public async Task<PagedResult<CourseListItemDto>> GetCoursesAsync(CourseFilterDto filter)
    {
        var query = dbSet
            .Include(c => c.Instructor)
            .Include(c => c.Category)
            .Include(c => c.Reviews)
            .Include(c => c.Enrollments)
            .Where(c => c.IsPublished)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(c => c.Title.Contains(filter.Search));
        }

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(c => c.CategoryId == filter.CategoryId);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(c => c.Price >= filter.MinPrice);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(c => c.Price <= filter.MaxPrice);
        }

        if (filter.InstructorId.HasValue)
        {
            query = query.Where(c => c.InstructorId == filter.InstructorId);
        }

        query = (filter.SortBy?.ToLower(), filter.SortDirection.ToLower()) switch
        {
            ("price", "desc") => query.OrderByDescending(c => c.Price),
            ("price", _) => query.OrderBy(c => c.Price),
            ("rating", "desc") => query.OrderByDescending(c => c.Reviews.Any() ? c.Reviews.Average(r => r.Rating) : 0),
            _ => query.OrderByDescending(c => c.CreatedAt)
        };

        var totalCount = await query.CountAsync();

        var courses = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PagedResult<CourseListItemDto>
        {
            Items = courses.Select(c => c.CourseToListItemMapper()).ToList(),
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };
    }
}