using LMS.Application.DTOs.Lessons;
using LMS.Domain.Entities;

public interface ILessonServices
{
    Task<ServicesResponse<IEnumerable<Lesson>>> GetLessonsBySectionAsync(int sectionId);
    Task<ServicesResponse<Lesson?>> GetLessonWithSectionAsync(int lessonId);
    Task<ServicesResponse<double>> GetTotalDurationByCourseAsync(int courseId);
    Task<ServicesResponse<int>> CountLessonsByCourseAsync(int courseId);

    Task<ServicesResponse<LessonResponseDto>> AddLessonAsync(int instructorId, LessonCreateDto dto);
    Task<ServicesResponse<bool>> UpdateLessonAsync(int lessonId, int instructorId, LessonUpdateDto dto);
    Task<ServicesResponse<bool>> DeleteLessonAsync(int lessonId, int instructorId);
    Task<ServicesResponse<LessonResponseDto>> GetLessonForStudentAsync(int lessonId, int studentId);
}