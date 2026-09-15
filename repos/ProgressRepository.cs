using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ProgressRepository(ApplicationDbContext context) : GenericRepository<LessonProgress>(context), IProgressRepository
{
    public async Task<int> GetCompletedLessonsCountAsync(String studentId, int courseId)
    {
        var hasAny = await dbSet.AnyAsync(p => p.StudentId == studentId
         && p.Lesson.Section.CourseId == courseId);
        if (!hasAny) return 0;
        return await dbSet.CountAsync(p => p.StudentId == studentId && p.Lesson.Section.CourseId == courseId);
    }

    public async Task<LessonProgress?> GetProgressAsync(String studentId, int lessonId)
    {
        var hasAny = await dbSet.AnyAsync(p => p.StudentId == studentId && p.LessonId == lessonId);
        if (!hasAny) return null;
        return await dbSet.FirstOrDefaultAsync(p => p.StudentId == studentId && p.LessonId == lessonId);
    }

    public async Task MarkLessonCompletedAsync(String studentId, int lessonId)
    {
        var progress = await GetProgressAsync(studentId, lessonId);

        if (progress is null)
        {
            progress = new LessonProgress
            {
                StudentId = studentId,
                LessonId = lessonId,
                IsCompleted = true,
                CompletedAt = DateTime.UtcNow
            };
            await dbSet.AddAsync(progress);
        }
        else if (!progress.IsCompleted)
        {
            progress.IsCompleted = true;
            progress.CompletedAt = DateTime.UtcNow;
        }

        await context.SaveChangesAsync();
    }
}