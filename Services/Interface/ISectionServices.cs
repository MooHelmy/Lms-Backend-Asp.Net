using LMS.Application.DTOs.Sections;
using LMS.Domain.Entities;

public interface ISectionServices
{
    Task<ServicesResponse<IEnumerable<Section>>> GetSectionsByCourseAsync(int courseId);
    Task<ServicesResponse<int>> GetMaxOrderAsync(int courseId);
    Task<ServicesResponse<bool>> IsOwnedByInstructorAsync(int sectionId, int instructorId);

    Task<ServicesResponse<SectionResponseDto>> AddSectionAsync(int instructorId, SectionCreateDto dto);
    Task<ServicesResponse<bool>> UpdateSectionAsync(int sectionId, int instructorId, SectionUpdateDto dto);
    Task<ServicesResponse<bool>> DeleteSectionAsync(int sectionId, int instructorId);
}