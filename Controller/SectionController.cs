using LMS.Application.DTOs.Sections;
using Microsoft.AspNetCore.Mvc;

public class SectionController(ISectionServices sectionServices) : ControllerBase
{
    public async Task<IActionResult> GetSectionsByCourse(int courseId)
    {
        var result = await sectionServices.GetSectionsByCourseAsync(courseId);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    public async Task<IActionResult> GetMaxOrder(int courseId)
    {
        var result = await sectionServices.GetMaxOrderAsync(courseId);
        return Ok(result);
    }
    public async Task<IActionResult> IsOwnedByInstructor(int sectionId, int instructorId)
    {
        var result = await sectionServices.IsOwnedByInstructorAsync(sectionId, instructorId);
        return Ok(result);
    }
    public async Task<IActionResult> AddSection(int instructorId, SectionCreateDto dto)
    {
        var result = await sectionServices.AddSectionAsync(instructorId, dto);
        return Ok(result);
    }
    public async Task<IActionResult> UpdateSection(int sectionId, int instructorId, SectionUpdateDto dto)
    {
        var result = await sectionServices.UpdateSectionAsync(sectionId, instructorId, dto);
        return Ok(result);
    }
    public async Task<IActionResult> DeleteSection(int sectionId, int instructorId)
    {
        var result = await sectionServices.DeleteSectionAsync(sectionId, instructorId);
        return Ok(result);
    }
}