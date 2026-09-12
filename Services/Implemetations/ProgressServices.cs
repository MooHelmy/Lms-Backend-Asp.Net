using LMS.Domain.Entities;

public class ProgressServices(IProgressRepository progressRepository) : IProgressServices
{
    public async Task<ServicesResponse<int>> GetCompletedLessonsCountAsync(int studentId, int courseId)
    {
        var completedLessonsCount = await progressRepository.GetCompletedLessonsCountAsync(studentId, courseId);
        if (completedLessonsCount == 0)
        {
            return new ServicesResponse<int>(false, "No completed lessons found for the specified student and course.", completedLessonsCount);
        }
        return new ServicesResponse<int>(true, "Completed lessons found for the specified student and course.", completedLessonsCount);
    }

    public async Task<ServicesResponse<LessonProgress?>> GetProgressAsync(int studentId, int lessonId)
    {
        var progress = await progressRepository.GetProgressAsync(studentId, lessonId);
        if (progress == null)
        {
            return new ServicesResponse<LessonProgress?>(false, "Progress not found.", null);
        }
        return new ServicesResponse<LessonProgress?>(true, "Progress found.", progress);
    }

    public async Task<ServicesResponse<bool>> MarkLessonCompletedAsync(int studentId, int lessonId)
    {
        var progress = await progressRepository.GetProgressAsync(studentId, lessonId);
        if (progress == null)
        {
            return new ServicesResponse<bool>(false, "Progress not found.", false);
        }
        return new ServicesResponse<bool>(true, "Lesson marked as completed.", true);
    }
}