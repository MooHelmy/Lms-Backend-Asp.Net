using LMS.Api.Controllers;
using LMS.Application.DTOs.Reviews;
using Microsoft.AspNetCore.Mvc;

public class ReviewController(IReviewServices reviewServices) : BaseApiController
{
    public async Task<IActionResult> GetReviewsByCourse(int courseId, int page, int pageSize)
    {
        var result = await reviewServices.GetReviewsByCourseAsync(courseId, page, pageSize);
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetAverageRating(int courseId)
    {
        var result = await reviewServices.GetAverageRatingAsync(courseId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> HasReviewed(int studentId, int courseId)
    {
        var result = await reviewServices.HasReviewedAsync(studentId, courseId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> AddReview(int studentId, ReviewCreateDto dto)
    {
        var result = await reviewServices.AddReviewAsync(studentId, dto);
        return HandleResponse(result);
    }

}