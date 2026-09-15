using LMS.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/subscriptions")]
[Authorize]
public class SubscriptionController(ISubscriptionServices subscriptionServices) : BaseApiController
{
    [HttpGet("my")]
    public async Task<IActionResult> GetActiveSubscription(String userId)
    {
        var result = await subscriptionServices.GetActiveSubscriptionAsync(userId);
        return HandleResponse(result);
    }
    [HttpGet("my/is-active")]
    public async Task<IActionResult> IsActiveSubscription(String userId)
    {
        var result = await subscriptionServices.IsActiveSubscriptionAsync(userId);
        return HandleResponse(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("expiring")]
    public async Task<IActionResult> GetExpiringSubscriptions(int daysBefore)
    {
        var result = await subscriptionServices.GetExpiringSubscriptionsAsync(daysBefore);
        return HandleResponse(result);
    }
    [HttpPost("plan/{planId:int}")]
    public async Task<IActionResult> Subscribe(String userId, int planId)
    {
        var result = await subscriptionServices.SubscribeAsync(userId, planId);
        return HandleResponse(result);
    }
    [HttpDelete("my")]
    public async Task<IActionResult> CancelSubscription(String userId)
    {
        var result = await subscriptionServices.CancelSubscriptionAsync(userId);
        return HandleResponse(result);
    }
}