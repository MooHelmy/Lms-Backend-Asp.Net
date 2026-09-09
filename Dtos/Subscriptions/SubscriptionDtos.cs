namespace LMS.Application.DTOs.Subscriptions
{
    public class SubscriptionPlanDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
    }

    public class SubscribeDto
    {
        public int PlanId { get; set; }
    }

    public class SubscriptionResponseDto
    {
        public int Id { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
