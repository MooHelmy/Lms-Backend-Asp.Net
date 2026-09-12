using LMS.Domain.Entities;

public class SubscriptionServices(ISubscriptionRepository subscriptionRepository) : ISubscriptionServices
{
    public async Task<ServicesResponse<Subscription?>> GetActiveSubscriptionAsync(int userId)
    {
        var subscription = await subscriptionRepository.GetActiveSubscriptionAsync(userId);
        if (subscription == null)
        {
            return new ServicesResponse<Subscription?>(false, "Subscription not found.", null);
        }
        return new ServicesResponse<Subscription?>(true, "Subscription found.", subscription);
    }

    public async Task<ServicesResponse<IEnumerable<Subscription>>> GetExpiringSubscriptionsAsync(int daysBefore)
    {
        var expiringSubscriptions = await subscriptionRepository.GetExpiringSubscriptionsAsync(daysBefore);
        if (expiringSubscriptions == null || !expiringSubscriptions.Any())
        {
            return new ServicesResponse<IEnumerable<Subscription>>(false, "No expiring subscriptions found.", null);
        }
        return new ServicesResponse<IEnumerable<Subscription>>(true, "Expiring subscriptions found.", expiringSubscriptions);
    }

    public async Task<ServicesResponse<bool>> IsActiveSubscriptionAsync(int userId)
    {
        var subscription = await subscriptionRepository.GetActiveSubscriptionAsync(userId);
        return new ServicesResponse<bool>(true, "Active subscription check completed.", subscription != null);
    }
}
