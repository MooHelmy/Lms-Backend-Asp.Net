using LMS.Domain.Entities;

public interface INotificationRepository : IGeneric<Notification>
{
    Task<IEnumerable<Notification>> GetByUserAsync(int userId, bool unreadOnly = false);
    Task MarkAsReadAsync(int notificationId);
    Task MarkAllAsReadAsync(int userId);
    Task<int> GetUnreadCountAsync(int userId);
}
