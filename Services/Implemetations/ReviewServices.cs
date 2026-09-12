using LMS.Domain.Entities;

public class ReviewServices(IReviewRepository reviewRepository) : IReviewServices
{
    public async Task<ServicesResponse<double>> GetAverageRatingAsync(int courseId)
    {
        var averageRating = await reviewRepository.GetAverageRatingAsync(courseId);
        return new ServicesResponse<double>(true, "Average rating calculated.", averageRating);
    }

    public async Task<ServicesResponse<IEnumerable<Review>>> GetReviewsByCourseAsync(int courseId, int page, int pageSize)
    {
        var reviews = await reviewRepository.GetReviewsByCourseAsync(courseId, page, pageSize);
        if (reviews == null || !reviews.Any())
        {
            return new ServicesResponse<IEnumerable<Review>>(false, "No reviews found for the specified course.", null);
        }
        return new ServicesResponse<IEnumerable<Review>>(true, "Reviews found for the specified course.", reviews);
    }

    public async Task<ServicesResponse<bool>> HasReviewedAsync(int studentId, int courseId)
    {
        var hasReviewed = await reviewRepository.HasReviewedAsync(studentId, courseId);
        return new ServicesResponse<bool>(true, "Review status determined.", hasReviewed);
    }
}
