using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ExamRepository(ApplicationDbContext context) : GenericRepository<Exam>(context), IExamRepository
{
    public async Task<IEnumerable<Exam>> GetExamsByCourseAsync(int courseId)
    {

        return await dbSet.Where(e => e.CourseId == courseId).ToListAsync();
    }

    public async Task<Exam?> GetExamWithQuestionsAsync(int examId)
    {

        return await dbSet.Include(e => e.Questions).ThenInclude(q => q.Answers)
        .FirstOrDefaultAsync(e => e.Id == examId);
    }

    public async Task<bool> IsOwnedByInstructorAsync(int examId, String instructorId)
    {

        return await dbSet.Include(e => e.Course)
            .AnyAsync(e => e.Id == examId && e.Course.InstructorId == instructorId);
    }
}