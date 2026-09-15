using LMS.Api.Controllers;
using LMS.Application.DTOs.Sections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/sections")]
[Authorize]
public class SectionController(ISectionServices sectionServices) : BaseApiController
{
    [HttpGet("course/{courseId:int}/sections")]
    public async Task<IActionResult> GetSectionsByCourse(int courseId)
    {
        var result = await sectionServices.GetSectionsByCourseAsync(courseId);
        return HandleResponse(result);
    }
    [HttpGet("course/{courseId:int}/max-order")]
    public async Task<IActionResult> GetMaxOrder(int courseId)
    {
        var result = await sectionServices.GetMaxOrderAsync(courseId);
        return HandleResponse(result);
    }
    [HttpGet("{sectionId:int}/owned-by-instructor")]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> IsOwnedByInstructor(int sectionId, String instructorId)
    {
        var result = await sectionServices.IsOwnedByInstructorAsync(sectionId, instructorId);
        return HandleResponse(result);
    }
    [HttpPost("course/{courseId:int}/add-section")]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> AddSection(String instructorId, SectionCreateDto dto)
    {
        var result = await sectionServices.AddSectionAsync(instructorId, dto);
        return HandleResponse(result);
    }
    [HttpPut("{sectionId:int}/update-section")]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> UpdateSection(int sectionId, String instructorId, SectionUpdateDto dto)
    {
        var result = await sectionServices.UpdateSectionAsync(sectionId, instructorId, dto);
        return HandleResponse(result);
    }
    [HttpDelete("{sectionId:int}/delete-section")]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> DeleteSection(int sectionId, String instructorId)
    {
        var result = await sectionServices.DeleteSectionAsync(sectionId, instructorId);
        return HandleResponse(result);
    }
}