using LMS.Domain.Entities;

public interface ISubscriptionServices
{
    Task<ServicesResponse<Subscription?>> GetActiveSubscriptionAsync(String userId);
    Task<ServicesResponse<bool>> IsActiveSubscriptionAsync(String userId);
    Task<ServicesResponse<IEnumerable<Subscription>>> GetExpiringSubscriptionsAsync(int daysBefore);

    Task<ServicesResponse<Subscription>> SubscribeAsync(String userId, int planId);
    Task<ServicesResponse<bool>> CancelSubscriptionAsync(String userId);
}