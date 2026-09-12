using LMS.Domain.Entities;

public interface INotificationServices
{
    Task<ServicesResponse<IEnumerable<Notification>>> GetByUserAsync(int userId, bool unreadOnly = false);
    Task<ServicesResponse<bool>> MarkAsReadAsync(int notificationId);
    Task<ServicesResponse<bool>> MarkAllAsReadAsync(int userId);
    Task<ServicesResponse<int>> GetUnreadCountAsync(int userId);
}
