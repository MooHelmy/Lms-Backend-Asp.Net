namespace LMS.Domain.Entities
{
    public class Subscription : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int PlanId { get; set; }
        public SubscriptionPlan Plan { get; set; } = null!;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
        public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;
    }
}
