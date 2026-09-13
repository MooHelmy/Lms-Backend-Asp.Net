using LMS.Application.DTOs.Lessons;
using LMS.Application.Mappers;
using LMS.Domain.Entities;

// محتاجين ISectionRepository عشان نتأكد من ملكية الكورس (عن طريق الـ Section)،
// ومحتاجين IEnrollmentRepository عشان نتأكد إن الطالب مسجل قبل ما نورّيه محتوى الدرس
public class LessonServices(
    ILessonRepository lessonRepository,
    ISectionRepository sectionRepository,
    IEnrollmentRepository enrollmentRepository) : ILessonServices
{
    // بيضيف درس جديد، بعد التأكد إن المدرس مالك الكورس اللي الـ Section ده تابعله
    public async Task<ServicesResponse<LessonResponseDto>> AddLessonAsync(int instructorId, LessonCreateDto dto)
    {
        var isOwned = await sectionRepository.IsOwnedByInstructorAsync(dto.SectionId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<LessonResponseDto>(false, "You do not own this course.");
        }

        var lesson = dto.LessonCreateToEntityMapper();
        await lessonRepository.CreateAsync(lesson);

        return new ServicesResponse<LessonResponseDto>(true, "Lesson created successfully.", lesson.LessonToResponseMapper());
    }

    // بيعدّل الدرس بعد التأكد من الملكية عن طريق الـ Section بتاعه
    public async Task<ServicesResponse<bool>> UpdateLessonAsync(int lessonId, int instructorId, LessonUpdateDto dto)
    {
        var lesson = await lessonRepository.GetLessonWithSectionAsync(lessonId);
        if (lesson is null)
        {
            return new ServicesResponse<bool>(false, "Lesson not found.");
        }

        var isOwned = await sectionRepository.IsOwnedByInstructorAsync(lesson.SectionId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<bool>(false, "You do not own this course.");
        }

        lesson.LessonUpdateMapper(dto);
        await lessonRepository.UpdateAsync(lesson);

        return new ServicesResponse<bool>(true, "Lesson updated successfully.", true);
    }

    public async Task<ServicesResponse<bool>> DeleteLessonAsync(int lessonId, int instructorId)
    {
        var lesson = await lessonRepository.GetLessonWithSectionAsync(lessonId);
        if (lesson is null)
        {
            return new ServicesResponse<bool>(false, "Lesson not found.");
        }

        var isOwned = await sectionRepository.IsOwnedByInstructorAsync(lesson.SectionId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<bool>(false, "You do not own this course.");
        }

        await lessonRepository.DeleteAsync(lessonId);
        return new ServicesResponse<bool>(true, "Lesson deleted successfully.", true);
    }

    // بيرجع محتوى الدرس للطالب، بس بعد التأكد إنه مسجل في الكورس اللي الدرس ده تابعله
    public async Task<ServicesResponse<LessonResponseDto>> GetLessonForStudentAsync(int lessonId, int studentId)
    {
        var lesson = await lessonRepository.GetLessonWithSectionAsync(lessonId);
        if (lesson is null)
        {
            return new ServicesResponse<LessonResponseDto>(false, "Lesson not found.");
        }

        var isEnrolled = await enrollmentRepository.IsEnrolledAsync(studentId, lesson.Section.CourseId);
        if (!isEnrolled)
        {
            return new ServicesResponse<LessonResponseDto>(false, "You are not enrolled in this course.");
        }

        return new ServicesResponse<LessonResponseDto>(true, "Lesson retrieved successfully.", lesson.LessonToResponseMapper());
    }


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