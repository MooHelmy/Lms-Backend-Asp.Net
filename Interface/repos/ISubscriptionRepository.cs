using LMS.Domain.Entities;

public interface ISubscriptionRepository : IGeneric<Subscription>
{
    Task<Subscription?> GetActiveSubscriptionAsync(String userId);
    Task<bool> IsActiveAsync(String userId);
    Task<IEnumerable<Subscription>> GetExpiringSubscriptionsAsync(int daysBefore);
}
