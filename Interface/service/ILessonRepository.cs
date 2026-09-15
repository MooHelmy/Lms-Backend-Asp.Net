using LMS.Application.DTOs.Lessons;
using LMS.Domain.Entities;

public interface ILessonServices
{
    Task<ServicesResponse<IEnumerable<Lesson>>> GetLessonsBySectionAsync(int sectionId);
    Task<ServicesResponse<Lesson?>> GetLessonWithSectionAsync(int lessonId);
    Task<ServicesResponse<double>> GetTotalDurationByCourseAsync(int courseId);
    Task<ServicesResponse<int>> CountLessonsByCourseAsync(int courseId);

    Task<ServicesResponse<LessonResponseDto>> AddLessonAsync(String instructorId, LessonCreateDto dto);
    Task<ServicesResponse<bool>> UpdateLessonAsync(int lessonId, String instructorId, LessonUpdateDto dto);
    Task<ServicesResponse<bool>> DeleteLessonAsync(int lessonId, String instructorId);
    Task<ServicesResponse<LessonResponseDto>> GetLessonForStudentAsync(int lessonId, String studentId);
}