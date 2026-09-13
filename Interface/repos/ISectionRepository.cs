using LMS.Domain.Entities;

public interface ISectionRepository : IGeneric<Section>
{
    Task<IEnumerable<Section>> GetSectionsByCourseAsync(int courseId);
    Task<int> GetMaxOrderAsync(int courseId);
    Task<bool> IsOwnedByInstructorAsync(int sectionId, int instructorId);
}
