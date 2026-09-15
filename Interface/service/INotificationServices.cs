using LMS.Application.DTOs.Notifications;
using LMS.Domain.Entities;

public interface INotificationServices
{
    Task<ServicesResponse<int>> AddNotificationAsync(CreateNotificationDto dto);
    Task<ServicesResponse<IEnumerable<Notification>>> GetByUserAsync(String userId, bool unreadOnly = false);
    Task<ServicesResponse<bool>> MarkAsReadAsync(int notificationId);
    Task<ServicesResponse<bool>> MarkAllAsReadAsync(String userId);
    Task<ServicesResponse<int>> GetUnreadCountAsync(String userId);
}
