using LMS.Domain.Entities;

public class SectionServices(ISectionRepository sectionRepository) : ISectionServices
{
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

    public async Task<ServicesResponse<bool>> IsOwnedByInstructorAsync(int sectionId, int instructorId)
    {
        var isOwned = await sectionRepository.IsOwnedByInstructorAsync(sectionId, instructorId);
        return new ServicesResponse<bool>(true, "Ownership check completed.", isOwned);
    }
}