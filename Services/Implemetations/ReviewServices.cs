using LMS.Application.DTOs.Reviews;
using LMS.Application.Mappers;
using LMS.Domain.Entities;

// محتاجين IEnrollmentRepository عشان نتأكد إن الطالب مسجل في الكورس قبل ما يسمحله يقيّمه
public class ReviewServices(IReviewRepository reviewRepository, IEnrollmentRepository enrollmentRepository) : IReviewServices
{
    // بيضيف تقييم جديد، بعد التأكد إن الطالب مسجل في الكورس ومقيّمهوش قبل كده
    public async Task<ServicesResponse<Review>> AddReviewAsync(String studentId, ReviewCreateDto dto)
    {
        var isEnrolled = await enrollmentRepository.IsEnrolledAsync(studentId, dto.CourseId);
        if (!isEnrolled)
        {
            return new ServicesResponse<Review>(false, "You must be enrolled to review this course.");
        }

        var hasReviewed = await reviewRepository.HasReviewedAsync(studentId, dto.CourseId);
        if (hasReviewed)
        {
            return new ServicesResponse<Review>(false, "You already reviewed this course.");
        }

        if (dto.Rating is < 1 or > 5)
        {
            return new ServicesResponse<Review>(false, "Rating must be between 1 and 5.");
        }

        var review = dto.ReviewCreateToEntityMapper(studentId);
        await reviewRepository.CreateAsync(review);

        return new ServicesResponse<Review>(true, "Review added successfully.", review);
    }


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

    public async Task<ServicesResponse<bool>> HasReviewedAsync(String studentId, int courseId)
    {
        var hasReviewed = await reviewRepository.HasReviewedAsync(studentId, courseId);
        return new ServicesResponse<bool>(true, "Review status determined.", hasReviewed);
    }
}