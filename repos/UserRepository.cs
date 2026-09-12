using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }

    // بيدور بالإيميل - مستخدمة وقت تسجيل الدخول
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    // بيتأكد إن الإيميل مش مستخدم قبل كده قبل السماح بالتسجيل
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await dbSet.AnyAsync(u => u.Email == email);
    }

    // كل المستخدمين اللي دورهم Instructor
    public async Task<IEnumerable<User>> GetInstructorsAsync()
    {
        return await dbSet.Where(u => u.Role == UserRole.Instructor).ToListAsync();
    }

    // كل المستخدمين اللي دورهم Student
    public async Task<IEnumerable<User>> GetStudentsAsync()
    {
        return await dbSet.Where(u => u.Role == UserRole.Student).ToListAsync();
    }

    // بيعطّل حساب المستخدم (بدل الحذف الفعلي) - استخدام الـ Admin
    public async Task DeactivateAsync(int userId)
    {
        var user = await dbSet.FindAsync(userId);
        if (user is null) return;

        user.IsActive = false;
        await context.SaveChangesAsync();
    }
}