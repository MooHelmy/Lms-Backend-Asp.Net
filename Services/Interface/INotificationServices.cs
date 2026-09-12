using LMS.Domain.Entities;

public interface INotificationServices
{
    Task<ServicesResponse<IEnumerable<Notification>>> GetByUserAsync(int userId, bool unreadOnly = false);
    Task<ServicesResponse> MarkAsReadAsync(int notificationId);
    Task<ServicesResponse> MarkAllAsReadAsync(int userId);
    Task<ServicesResponse> GetUnreadCountAsync(int userId);
}
