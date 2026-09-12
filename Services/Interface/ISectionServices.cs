using LMS.Domain.Entities;

public interface ISectionServices
{
    Task<ServicesResponse<IEnumerable<Section>>> GetSectionsByCourseAsync(int courseId);
    Task<ServicesResponse<int>> GetMaxOrderAsync(int courseId);
    Task<ServicesResponse<bool>> IsOwnedByInstructorAsync(int sectionId, int instructorId);
}
