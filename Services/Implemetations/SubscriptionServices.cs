using LMS.Domain.Entities;

// ملاحظة: مفيش ISubscriptionPlanRepository مخصص للـ SubscriptionPlan، فاستخدمنا IGeneric<SubscriptionPlan>
// مباشرة (نفس فكرة الـ Generic Repository) عشان نجيب الخطة ونعرف مدتها بالأيام.
public class SubscriptionServices(
    ISubscriptionRepository subscriptionRepository,
    IGeneric<SubscriptionPlan> subscriptionPlanRepository) : ISubscriptionServices
{
    // بيشترك المستخدم في خطة معينة، وبيحسب تاريخ الانتهاء بناءً على مدة الخطة 
    public async Task<ServicesResponse<Subscription>> SubscribeAsync(String userId, int planId)
    {
        var plan = await subscriptionPlanRepository.GetByIdAsync(planId);
        if (plan is null)
        {
            return new ServicesResponse<Subscription>(false, "Subscription plan not found.");
        }

        var subscription = new Subscription
        {
            UserId = userId,
            PlanId = planId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(plan.DurationInDays),
            Status = SubscriptionStatus.Active
        };

        await subscriptionRepository.CreateAsync(subscription);

        return new ServicesResponse<Subscription>(true, "Subscribed successfully.", subscription);
    }

    // بيلغي الاشتراك الفعال الحالي للمستخدم
    public async Task<ServicesResponse<bool>> CancelSubscriptionAsync(String userId)
    {
        var subscription = await subscriptionRepository.SingleOrDefaultAsync(
            s => s.UserId == userId && s.Status == SubscriptionStatus.Active);

        if (subscription is null)
        {
            return new ServicesResponse<bool>(false, "No active subscription found.");
        }

        subscription.Status = SubscriptionStatus.Cancelled;
        await subscriptionRepository.UpdateAsync(subscription);

        return new ServicesResponse<bool>(true, "Subscription cancelled successfully.", true);
    }


    public async Task<ServicesResponse<Subscription?>> GetActiveSubscriptionAsync(String userId)
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

    public async Task<ServicesResponse<bool>> IsActiveSubscriptionAsync(String userId)
    {
        var subscription = await subscriptionRepository.GetActiveSubscriptionAsync(userId);
        return new ServicesResponse<bool>(true, "Active subscription check completed.", subscription != null);
    }
}