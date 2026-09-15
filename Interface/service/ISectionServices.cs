using LMS.Application.DTOs.Sections;
using LMS.Domain.Entities;

public interface ISectionServices
{
    Task<ServicesResponse<IEnumerable<Section>>> GetSectionsByCourseAsync(int courseId);
    Task<ServicesResponse<int>> GetMaxOrderAsync(int courseId);
    Task<ServicesResponse<bool>> IsOwnedByInstructorAsync(int sectionId, String instructorId);

    Task<ServicesResponse<SectionResponseDto>> AddSectionAsync(String instructorId, SectionCreateDto dto);
    Task<ServicesResponse<bool>> UpdateSectionAsync(int sectionId, String instructorId, SectionUpdateDto dto);
    Task<ServicesResponse<bool>> DeleteSectionAsync(int sectionId, String instructorId);
}