using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class NotificationRepository(DbContext context) : GenericRepository<Notification>(context), INotificationRepository
{
    public async Task<IEnumerable<Notification>> GetByUserAsync(int userId, bool unreadOnly = false)
    {
        var query = dbSet.Where(n => n.UserId == userId);

        if (unreadOnly)
            query = query.Where(n => !n.IsRead);

        return await query.OrderByDescending(n => n.CreatedAt).ToListAsync();
    }

    public async Task<int> GetUnreadCountAsync(int userId)
    {
        return await dbSet.CountAsync(n => n.UserId == userId && !n.IsRead);
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        var notifications = await dbSet.Where(n => n.UserId == userId && !n.IsRead).ToListAsync();

        foreach (var n in notifications)
            n.IsRead = true;

        await context.SaveChangesAsync();
    }



    public async Task MarkAsReadAsync(int notificationId)
    {
        var notfication = await dbSet.FindAsync(notificationId);
        if (notfication is null) return;

        notfication.IsRead = true;
        await context.SaveChangesAsync();

    }
}