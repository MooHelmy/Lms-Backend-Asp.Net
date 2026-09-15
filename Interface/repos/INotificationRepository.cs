using LMS.Domain.Entities;

public interface INotificationRepository : IGeneric<Notification>
{
    Task<IEnumerable<Notification>> GetByUserAsync(String userId, bool unreadOnly = false);
    Task MarkAsReadAsync(int notificationId);
    Task MarkAllAsReadAsync(String userId);
    Task<int> GetUnreadCountAsync(String userId);
}
