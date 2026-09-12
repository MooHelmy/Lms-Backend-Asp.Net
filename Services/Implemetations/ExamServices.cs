using LMS.Domain.Entities;

public class ExamServices(IExamRepository examRepository) : IExamServices
{
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

    public async Task<ServicesResponse> IsOwnedByInstructorAsync(int examId, int instructorId)
    {
        var isOwned = await examRepository.IsOwnedByInstructorAsync(examId, instructorId);
        if (!isOwned)
        {
            return new ServicesResponse(false, "Exam not found or not owned by the specified instructor.");
        }
        return new ServicesResponse(true, "Exam found and owned by the specified instructor.");
    }
}