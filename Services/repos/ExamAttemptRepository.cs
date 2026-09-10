using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ExamAttemptRepository(DbContext context) : GenericRepository<ExamAttempt>(context), IExamAttemptRepository
{
    public async Task<IEnumerable<ExamAttempt>> GetAttemptsByStudentAsync(int studentId, int examId)
    {
        return await dbSet.Include(e => e.Exam).Include(e => e.Student)
        .Where(e => e.StudentId == studentId && e.ExamId == examId)
        .OrderByDescending(a => a.StartedAt)
            .ToListAsync();

    }

    public async Task<int> GetAttemptsCountAsync(int studentId, int examId)
    {
        return await dbSet.CountAsync(e => e.StudentId == studentId && e.ExamId == examId);
    }

    public async Task<ExamAttempt?> GetAttemptWithAnswersAsync(int attemptId)
    {
        return await dbSet.Include(e => e.SelectedAnswers)
        .Include(a => a.Exam).FirstOrDefaultAsync(e => e.Id == attemptId);
    }

    public async Task<ExamAttempt?> GetLatestAttemptAsync(int studentId, int examId)
    {
        return await dbSet.Include(e => e.Exam).Include(e => e.Student)
            .Where(e => e.StudentId == studentId && e.ExamId == examId)
            .OrderByDescending(e => e.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> HasPassedAsync(int studentId, int examId)
    {
        return await dbSet.AnyAsync(e => e.StudentId == studentId && e.ExamId == examId && e.Score >= 70);
    }
}