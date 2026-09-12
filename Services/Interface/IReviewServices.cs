using LMS.Domain.Entities;

public interface IReviewServices
{
    Task<ServicesResponse<IEnumerable<Review>>> GetReviewsByCourseAsync(int courseId, int page, int pageSize);
    Task<ServicesResponse> GetAverageRatingAsync(int courseId);
    Task<ServicesResponse> HasReviewedAsync(int studentId, int courseId);
}
