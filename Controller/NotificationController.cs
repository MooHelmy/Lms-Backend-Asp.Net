using LMS.Api.Controllers;
using LMS.Application.DTOs.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/notifications")]
[Authorize]
public class NotificationController(INotificationServices notificationServices) : BaseApiController
{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddNotification(CreateNotificationDto dto)
    {
        var result = await notificationServices.AddNotificationAsync(dto);
        return HandleResponse(result);
    }
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId, bool unreadOnly = false)
    {
        var result = await notificationServices.GetByUserAsync(userId, unreadOnly);
        return HandleResponse(result);
    }
    [HttpPut("{id:int}/read")]
    public async Task<IActionResult> MarkAsRead(int notificationId)
    {
        var result = await notificationServices.MarkAsReadAsync(notificationId);
        return HandleResponse(result);
    }
    [HttpPut("read-all")]
    public async Task<IActionResult> MarkAllAsRead(int userId)
    {
        var result = await notificationServices.MarkAllAsReadAsync(userId);
        return HandleResponse(result);
    }
    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount(int userId)
    {
        var result = await notificationServices.GetUnreadCountAsync(userId);
        return HandleResponse(result);
    }
}