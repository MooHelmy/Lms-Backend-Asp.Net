using LMS.Application.DTOs.Exams;
using LMS.Application.Mappers;
using LMS.Domain.Entities;

// محتاجين ICourseRepository عشان نتأكد إن المدرس مالك الكورس قبل ما ننشئله امتحان جديد
public class ExamServices(IExamRepository examRepository, ICourseRepository courseRepository) : IExamServices
{
    // بينشئ امتحان جديد لكورس، بعد التأكد إن المدرس مالك الكورس ده
    public async Task<ServicesResponse<Exam>> CreateExamAsync(String instructorId, ExamCreateDto dto)
    {
        var isOwned = await courseRepository.IsOwnedByInstructorAsync(dto.CourseId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<Exam>(false, "You do not own this course.");
        }

        var exam = dto.ExamCreateToEntityMapper();
        await examRepository.CreateAsync(exam);

        return new ServicesResponse<Exam>(true, "Exam created successfully.", exam);
    }

    // بيعدّل الامتحان بعد التأكد من الملكية عن طريق الكورس المرتبط بيه
    public async Task<ServicesResponse<bool>> UpdateExamAsync(int examId, String instructorId, ExamUpdateDto dto)
    {
        var isOwned = await examRepository.IsOwnedByInstructorAsync(examId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<bool>(false, "You do not own this course.");
        }

        var exam = await examRepository.GetByIdAsync(examId);
        if (exam is null)
        {
            return new ServicesResponse<bool>(false, "Exam not found.");
        }

        exam.ExamUpdateMapper(dto);
        await examRepository.UpdateAsync(exam);

        return new ServicesResponse<bool>(true, "Exam updated successfully.", true);
    }

    // بيحذف الامتحان بنفس تأكيد الملكية
    public async Task<ServicesResponse<bool>> DeleteExamAsync(int examId, String instructorId)
    {
        var isOwned = await examRepository.IsOwnedByInstructorAsync(examId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse<bool>(false, "You do not own this course.");
        }

        await examRepository.DeleteAsync(examId);
        return new ServicesResponse<bool>(true, "Exam deleted successfully.", true);
    }


    public async Task<ServicesResponse<IEnumerable<Exam>>> GetExamsByCourseAsync(int courseId)
    {
        var exams = await examRepository.GetExamsByCourseAsync(courseId);
        if (exams == null || !exams.Any())
        {
            return new ServicesResponse<IEnumerable<Exam>>(false, "No exams found for the specified course.", null);
        }
        return new ServicesResponse<IEnumerable<Exam>>(true, "Exams found for the specified course.", exams);
    }

    public async Task<ServicesResponse<Exam?>> GetExamWithQuestionsAsync(int examId)
    {
        var exam = await examRepository.GetExamWithQuestionsAsync(examId);
        if (exam == null)
        {
            return new ServicesResponse<Exam?>(false, "Exam not found.", null);
        }
        return new ServicesResponse<Exam?>(true, "Exam found.", exam);
    }

    public async Task<ServicesResponse> IsOwnedByInstructorAsync(int examId, String instructorId)
    {
        var isOwned = await examRepository.IsOwnedByInstructorAsync(examId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse(false, "Exam not found or not owned by the specified instructor.");
        }
        return new ServicesResponse(true, "Exam found and owned by the specified instructor.");
    }
}