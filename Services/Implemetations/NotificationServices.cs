using LMS.Domain.Entities;

public class NotificationServices(INotificationRepository notificationRepository) : INotificationServices
{
    public async Task<ServicesResponse<IEnumerable<Notification>>> GetByUserAsync(int userId, bool unreadOnly = false)
    {
        var notifications = await notificationRepository.GetByUserAsync(userId, unreadOnly);
        if (notifications == null || !notifications.Any())
        {
            return new ServicesResponse<IEnumerable<Notification>>(false, "No notifications found for the specified user.", null);
        }
        return new ServicesResponse<IEnumerable<Notification>>(true, "Notifications found for the specified user.", notifications);
    }

    public async Task<ServicesResponse<int>> GetUnreadCountAsync(int userId)
    {
        var unreadCount = await notificationRepository.GetUnreadCountAsync(userId);
        if (unreadCount == 0)
        {
            return new ServicesResponse<int>(false, "No unread notifications found for the specified user.", unreadCount);
        }
        return new ServicesResponse<int>(true, "Unread notifications found for the specified user.", unreadCount);
    }

    public async Task<ServicesResponse<bool>> MarkAllAsReadAsync(int userId)
    {
        await notificationRepository.MarkAllAsReadAsync(userId);
        return new ServicesResponse<bool>(true, "All notifications marked as read for the specified user.");
    }

    public async Task<ServicesResponse<bool>> MarkAsReadAsync(int notificationId)
    {
        await notificationRepository.MarkAsReadAsync(notificationId);
        return new ServicesResponse<bool>(true, "Notification marked as read.");
    }
}