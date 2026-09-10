using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class SectionRepository(DbContext context) : GenericRepository<Section>(context), ISectionRepository
{
    public async Task<int> GetMaxOrderAsync(int courseId)
    {
        var hasAny = await dbSet.AnyAsync(s => s.CourseId == courseId);
        if (!hasAny) return 0;

        return await dbSet
            .Where(s => s.CourseId == courseId)
            .MaxAsync(s => s.Order);
    }

    public async Task<IEnumerable<Section>> GetSectionsByCourseAsync(int courseId)
    {
        return await dbSet
                   .Where(s => s.CourseId == courseId)
                   .OrderBy(s => s.Order)
                   .ToListAsync();
    }

    public async Task<bool> IsOwnedByInstructorAsync(int sectionId, int instructorId)
    {
        return await dbSet.Include(s => s.Course)
            .AnyAsync(s => s.Id == sectionId && s.Course.InstructorId == instructorId);
    }
}