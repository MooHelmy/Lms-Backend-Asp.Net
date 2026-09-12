using LMS.Domain.Entities;

public class LessonServices(ILessonRepository lessonRepository) : ILessonServices
{
    public async Task<ServicesResponse<int>> CountLessonsByCourseAsync(int courseId)
    {
        var count = await lessonRepository.CountLessonsByCourseAsync(courseId);
        if (count == 0)
        {
            return new ServicesResponse<int>(false, "No lessons found for the specified course.", count);
        }
        return new ServicesResponse<int>(true, "Lessons found for the specified course.", count);
    }

    public async Task<ServicesResponse<IEnumerable<Lesson>>> GetLessonsBySectionAsync(int sectionId)
    {
        var lessons = await lessonRepository.GetLessonsBySectionAsync(sectionId);
        if (lessons == null || !lessons.Any())
        {
            return new ServicesResponse<IEnumerable<Lesson>>(false, "No lessons found for the specified section.", lessons);
        }
        return new ServicesResponse<IEnumerable<Lesson>>(true, "Lessons found for the specified section.", lessons);
    }

    public async Task<ServicesResponse<Lesson?>> GetLessonWithSectionAsync(int lessonId)
    {
        var lesson = await lessonRepository.GetLessonWithSectionAsync(lessonId);
        if (lesson == null)
        {
            return new ServicesResponse<Lesson?>(false, "Lesson not found.", null);
        }
        return new ServicesResponse<Lesson?>(true, "Lesson found.", lesson);
    }

    public async Task<ServicesResponse<double>> GetTotalDurationByCourseAsync(int courseId)
    {
        var totalDuration = await lessonRepository.GetTotalDurationByCourseAsync(courseId);
        return new ServicesResponse<double>(true, "Total duration calculated.", totalDuration);
    }
}


