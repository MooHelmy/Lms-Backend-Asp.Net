using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class EnrollmentRepository(DbContext context) : GenericRepository<Enrollment>(context), IEnrollmentRepository
{
    public async Task<int> GetActiveEnrollmentsCountAsync(int courseId)
    {
        return await dbSet.CountAsync(e => e.CourseId == courseId && e.Status == EnrollmentStatus.Active);
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseAsync(int courseId)
    {
        return await dbSet.Include(e => e.Student)
            .Where(e => e.CourseId == courseId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId)
    {
        return await dbSet.Include(e => e.Course)
            .Where(e => e.StudentId == studentId)
            .ToListAsync();
    }

    public async Task<bool> IsEnrolledAsync(int studentId, int courseId)
    {
        return await dbSet.AnyAsync(e =>
             e.StudentId == studentId &&
             e.CourseId == courseId &&
             e.Status == EnrollmentStatus.Active);
    }

    public async Task MarkAsCompletedAsync(int enrollmentId)
    {
        var enrollment = await dbSet.FindAsync(enrollmentId);
        if (enrollment is null) return;

        enrollment.Status = EnrollmentStatus.Completed;
        enrollment.CompletedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
}