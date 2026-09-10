using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class SubscriptionRepository(DbContext context) : GenericRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<Subscription?> GetActiveSubscriptionAsync(int userId)
    {
        var hasAny = await dbSet.AnyAsync(s => s.UserId == userId);
        if (!hasAny) return null;

        return await dbSet
            .Include(s => s.Plan)
            .Where(s => s.UserId == userId && s.Status == SubscriptionStatus.Active
            && s.EndDate > DateTime.UtcNow)
            .OrderByDescending(s => s.EndDate)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Subscription>> GetExpiringSubscriptionsAsync(int daysBefore)
    {
        var targetDate = DateTime.UtcNow.AddDays(daysBefore);

        return await dbSet
            .Include(s => s.User)
            .Where(s => s.Status == SubscriptionStatus.Active && s.EndDate <= targetDate
             && s.EndDate > DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<bool> IsActiveAsync(int userId)
    {
        var hasAny = await dbSet.AnyAsync(s => s.UserId == userId);
        if (!hasAny) return false;
        return await dbSet.AnyAsync(s => s.UserId == userId && s.EndDate > DateTime.UtcNow);

    }
}