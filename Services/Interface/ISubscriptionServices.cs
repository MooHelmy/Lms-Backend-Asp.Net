using LMS.Domain.Entities;

public interface ISubscriptionServices
{
    Task<ServicesResponse<Subscription?>> GetActiveSubscriptionAsync(int userId);
    Task<ServicesResponse<bool>> IsActiveSubscriptionAsync(int userId);
    Task<ServicesResponse<IEnumerable<Subscription>>> GetExpiringSubscriptionsAsync(int daysBefore);
}
