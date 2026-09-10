using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ReviewRepository(DbContext context) : GenericRepository<Review>(context), IReviewRepository
{
    public async Task<double> GetAverageRatingAsync(int courseId)
    {
        var hasReviews = await dbSet.AnyAsync(r => r.CourseId == courseId);
        if (!hasReviews) return 0;

        return await dbSet.Where(r => r.CourseId == courseId).AverageAsync(r => r.Rating);
    }

    public async Task<IEnumerable<Review>> GetReviewsByCourseAsync(int courseId, int page, int pageSize)
    {
        return await dbSet.Include(r => r.Student)
            .Where(r => r.CourseId == courseId)
            .OrderByDescending(r => r.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<bool> HasReviewedAsync(int studentId, int courseId)
    {
        return await dbSet.AnyAsync(r => r.StudentId == studentId && r.CourseId == courseId);
    }

}