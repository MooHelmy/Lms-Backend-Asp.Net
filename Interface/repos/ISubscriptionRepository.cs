using LMS.Domain.Entities;

public interface ISubscriptionRepository : IGeneric<Subscription>
{
    Task<Subscription?> GetActiveSubscriptionAsync(int userId);
    Task<bool> IsActiveAsync(int userId);
    Task<IEnumerable<Subscription>> GetExpiringSubscriptionsAsync(int daysBefore);
}
