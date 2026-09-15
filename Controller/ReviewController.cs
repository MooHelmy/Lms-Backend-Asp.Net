using LMS.Api.Controllers;
using LMS.Application.DTOs.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/reviews")]

public class ReviewController(IReviewServices reviewServices) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet("course/{courseId:int}")]
    public async Task<IActionResult> GetReviewsByCourse(int courseId, int page, int pageSize)
    {
        var result = await reviewServices.GetReviewsByCourseAsync(courseId, page, pageSize);
        return HandleResponse(result);
    }
    [AllowAnonymous]
    [HttpGet("course/{courseId:int}/average-rating")]
    public async Task<IActionResult> GetAverageRating(int courseId)
    {
        var result = await reviewServices.GetAverageRatingAsync(courseId);
        return HandleResponse(result);
    }
    [Authorize(Roles = "Student")]
    [HttpGet("course/{courseId:int}/has-reviewed")]
    public async Task<IActionResult> HasReviewed(int studentId, int courseId)
    {
        var result = await reviewServices.HasReviewedAsync(studentId, courseId);
        return HandleResponse(result);
    }
    [Authorize(Roles = "Student")]
    [HttpPost("course/{courseId:int}/add-review")]
    public async Task<IActionResult> AddReview(int studentId, ReviewCreateDto dto)
    {
        var result = await reviewServices.AddReviewAsync(studentId, dto);
        return HandleResponse(result);
    }

}