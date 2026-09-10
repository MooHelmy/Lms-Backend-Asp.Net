using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class CertificateRepository(DbContext context) : GenericRepository<Certificate>(context), ICertificateRepository
{
    // بيتأكد إن الشهادة معملتش قبل كده لنفس الطالب في نفس الكورس (منع تكرار الإصدار)

    public async Task<bool> ExistsForStudentCourseAsync(int studentId, int courseId)
    {
        return await dbSet.AnyAsync(c => c.StudentId == studentId && c.CourseId == courseId);
    }

    public async Task<Certificate?> GetByCertificateNumberAsync(string certificateNumber)
    {
        return await dbSet
           .Include(c => c.Student)
           .Include(c => c.Course).ThenInclude(co => co.Instructor)
           .FirstOrDefaultAsync(c => c.CertificateNumber == certificateNumber);
    }

    public async Task<IEnumerable<Certificate>> GetCertificatesByStudentAsync(int studentId)
    {
        return await dbSet.Include(c => c.Student)
             .Include(c => c.Course)
             .Where(c => c.StudentId == studentId)
             .ToListAsync();
    }
}