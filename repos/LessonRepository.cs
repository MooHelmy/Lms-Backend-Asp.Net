using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class LessonRepository(DbContext context) : GenericRepository<Lesson>(context), ILessonRepository
{
    public async Task<int> CountLessonsByCourseAsync(int courseId)
    {
        return await dbSet.CountAsync(l => l.Section.CourseId == courseId);

    }

    public async Task<IEnumerable<Lesson>> GetLessonsBySectionAsync(int sectionId)
    {
        return await dbSet.Where(l => l.SectionId == sectionId).ToListAsync();
    }

    public async Task<Lesson?> GetLessonWithSectionAsync(int lessonId)
    {
        return await dbSet.Include(l => l.Section).FirstOrDefaultAsync(l => l.Id == lessonId);
    }

    public async Task<int> GetTotalDurationByCourseAsync(int courseId)
    {
        return await dbSet.Where(L => L.Section.CourseId == courseId)
        .SumAsync(L => L.DurationInSeconds);

    }
}