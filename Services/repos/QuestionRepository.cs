using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class QuestionRepository(DbContext context) : GenericRepository<Question>(context), IQuestionRepository
{
    public async Task<IEnumerable<Question>> GetQuestionsByExamAsync(int examId)
    {
        return await dbSet.Include(q => q.Answers)
            .Where(q => q.ExamId == examId)
            .OrderBy(q => q.Order)
            .ToListAsync();
    }

    public async Task<int> GetTotalPointsAsync(int examId)
    {
        return await dbSet.Where(q => q.ExamId == examId).SumAsync(q => q.Points);
    }

    public async Task<Answer?> GetCorrectAnswerAsync(int questionId)
    {
        return await context.Set<Answer>()
           .FirstOrDefaultAsync(a => a.QuestionId == questionId && a.IsCorrect);
    }
}