using LMS.Application.DTOs.Progress;
using LMS.Application.Mappers;
using LMS.Domain.Entities;

// محتاجين ILessonRepository عشان نعرف العدد الكلي لدروس الكورس ونحسب نسبة الـ Progress
public class ProgressServices(IProgressRepository progressRepository, ILessonRepository lessonRepository) : IProgressServices
{
    // بيحدّث تقدم الطالب في درس معين (وقت المشاهدة + هل خلص الدرس ولا لأ)
    // لو مفيش سجل تقدم أصلًا بينشئ واحد جديد، ولو موجود بيحدّثه بس
    public async Task<ServicesResponse<bool>> UpdateLessonProgressAsync(int studentId, UpdateLessonProgressDto dto)
    {
        var progress = await progressRepository.GetProgressAsync(studentId, dto.LessonId);

        if (progress is null)
        {
            var newProgress = dto.LessonProgressCreateMapper(studentId);
            await progressRepository.CreateAsync(newProgress);
        }
        else
        {
            progress.LessonProgressUpdateMapper(dto);
            await progressRepository.UpdateAsync(progress);
        }

        return new ServicesResponse<bool>(true, "Lesson progress updated successfully.", true);
    }

    // بيحسب نسبة تقدم الطالب في كورس معين: عدد الدروس المكتملة / إجمالي عدد الدروس
    public async Task<ServicesResponse<CourseProgressDto>> GetCourseProgressAsync(int studentId, int courseId)
    {
        var totalLessons = await lessonRepository.CountLessonsByCourseAsync(courseId);
        var completedLessons = await progressRepository.GetCompletedLessonsCountAsync(studentId, courseId);

        var percentage = totalLessons == 0
            ? 0
            : (int)Math.Round(completedLessons * 100.0 / totalLessons);

        var result = new CourseProgressDto
        {
            CourseId = courseId,
            CompletedLessons = completedLessons,
            TotalLessons = totalLessons,
            ProgressPercentage = percentage
        };

        return new ServicesResponse<CourseProgressDto>(true, "Course progress calculated.", result);
    }


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

    // كانت الميثود دي بترجع true/false بس من غير ما تنادي التحديث الفعلي في الـ Repository
    // (اللي أصلًا بيعمل Create لو مفيش سجل، أو Update لو موجود) - اتصلحت هنا
    public async Task<ServicesResponse<bool>> MarkLessonCompletedAsync(int studentId, int lessonId)
    {
        await progressRepository.MarkLessonCompletedAsync(studentId, lessonId);
        return new ServicesResponse<bool>(true, "Lesson marked as completed.", true);
    }
}