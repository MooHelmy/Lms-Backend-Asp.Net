using LMS.Domain.Entities;

public interface ISectionServices
{
    Task<ServicesResponse<IEnumerable<Section>>> GetSectionsByCourseAsync(int courseId);
    Task<ServicesResponse> GetMaxOrderAsync(int courseId);
    Task<ServicesResponse> IsOwnedByInstructorAsync(int sectionId, int instructorId);
}
