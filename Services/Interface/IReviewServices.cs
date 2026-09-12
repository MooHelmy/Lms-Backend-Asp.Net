using LMS.Domain.Entities;

public interface IReviewServices
{
    Task<ServicesResponse<IEnumerable<Review>>> GetReviewsByCourseAsync(int courseId, int page, int pageSize);
    Task<ServicesResponse<double>> GetAverageRatingAsync(int courseId);
    Task<ServicesResponse<bool>> HasReviewedAsync(int studentId, int courseId);
}
