using LMS.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

public class SubscriptionController(ISubscriptionServices subscriptionServices) : BaseApiController
{
    public async Task<IActionResult> GetActiveSubscription(int userId)
    {
        var result = await subscriptionServices.GetActiveSubscriptionAsync(userId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> IsActiveSubscription(int userId)
    {
        var result = await subscriptionServices.IsActiveSubscriptionAsync(userId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetExpiringSubscriptions(int daysBefore)
    {
        var result = await subscriptionServices.GetExpiringSubscriptionsAsync(daysBefore);
        return HandleResponse(result);
    }
    public async Task<IActionResult> Subscribe(int userId, int planId)
    {
        var result = await subscriptionServices.SubscribeAsync(userId, planId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> CancelSubscription(int userId)
    {
        var result = await subscriptionServices.CancelSubscriptionAsync(userId);
        return HandleResponse(result);
    }
}