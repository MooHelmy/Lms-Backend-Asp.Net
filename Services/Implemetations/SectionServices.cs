using LMS.Application.DTOs.Sections;
using LMS.Application.Mappers;
using LMS.Domain.Entities;

// محتاجين ICourseRepository كمان عشان نتأكد إن الكورس ده ملك المدرس قبل ما نضيفله Section جديد
public class SectionServices(ISectionRepository sectionRepository, ICourseRepository courseRepository) : ISectionServices
{
    // بيضيف Section جديد، وبيتأكد الأول إن المدرس فعلًا مالك الكورس اللي هيتضاف له
    public async Task<ServicesResponse<SectionResponseDto>> AddSectionAsync(String instructorId, SectionCreateDto dto)
    {
        var isOwned = await courseRepository.IsOwnedByInstructorAsync(dto.CourseId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<SectionResponseDto>(false, "You do not own this course.");
        }

        var section = dto.SectionCreateToEntityMapper();
        await sectionRepository.CreateAsync(section);

        return new ServicesResponse<SectionResponseDto>(true, "Section created successfully.", section.SectionToResponseMapper());
    }

    public async Task<ServicesResponse<bool>> UpdateSectionAsync(int sectionId, String instructorId, SectionUpdateDto dto)
    {
        var isOwned = await sectionRepository.IsOwnedByInstructorAsync(sectionId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<bool>(false, "You do not own this section.");
        }

        var section = await sectionRepository.GetByIdAsync(sectionId);
        if (section is null)
        {
            return new ServicesResponse<bool>(false, "Section not found.");
        }

        section.SectionUpdateMapper(dto);
        await sectionRepository.UpdateAsync(section);

        return new ServicesResponse<bool>(true, "Section updated successfully.", true);
    }

    public async Task<ServicesResponse<bool>> DeleteSectionAsync(int sectionId, String instructorId)
    {
        var isOwned = await sectionRepository.IsOwnedByInstructorAsync(sectionId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<bool>(false, "You do not own this section.");
        }

        await sectionRepository.DeleteAsync(sectionId);
        return new ServicesResponse<bool>(true, "Section deleted successfully.", true);
    }


    public async Task<ServicesResponse<int>> GetMaxOrderAsync(int courseId)
    {
        var maxOrder = await sectionRepository.GetMaxOrderAsync(courseId);
        return new ServicesResponse<int>(true, "Max order calculated.", maxOrder);
    }

    public async Task<ServicesResponse<IEnumerable<Section>>> GetSectionsByCourseAsync(int courseId)
    {
        var sections = await sectionRepository.GetSectionsByCourseAsync(courseId);
        if (sections == null || !sections.Any())
        {
            return new ServicesResponse<IEnumerable<Section>>(false, "No sections found for the specified course.", null);
        }
        return new ServicesResponse<IEnumerable<Section>>(true, "Sections found for the specified course.", sections);
    }

    public async Task<ServicesResponse<bool>> IsOwnedByInstructorAsync(int sectionId, String instructorId)
    {
        var isOwned = await sectionRepository.IsOwnedByInstructorAsync(sectionId, instructorId);
        return new ServicesResponse<bool>(true, "Ownership check completed.", isOwned);
    }
}