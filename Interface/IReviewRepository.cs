using LMS.Domain.Entities;

public interface IReviewRepository : IGeneric<Review>
{
    Task<IEnumerable<Review>> GetReviewsByCourseAsync(int courseId, int page, int pageSize);
    Task<double> GetAverageRatingAsync(int courseId);
    Task<bool> HasReviewedAsync(int studentId, int courseId);
}
