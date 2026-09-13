using LMS.Application.DTOs.ExamAttempts;
using LMS.Application.Mappers;
using LMS.Domain.Entities;

// محتاجين IExamRepository عشان نجيب أسئلة/إجابات الامتحان وقت التصحيح،
// ومحتاجين IEnrollmentRepository عشان نتأكد إن الطالب مسجل قبل ما يبدأ الامتحان
public class ExamAttemptServices(
    IExamAttemptRepository examAttemptRepository,
    IExamRepository examRepository,
    IEnrollmentRepository enrollmentRepository) : IExamAttemptServices
{
    // بيبدأ محاولة امتحان جديدة، بعد التأكد إن الطالب مسجل في كورس الامتحان ده
    public async Task<ServicesResponse<ExamAttemptResultDto>> StartAttemptAsync(int studentId, StartExamAttemptDto dto)
    {
        var exam = await examRepository.GetByIdAsync(dto.ExamId);
        if (exam is null)
        {
            return new ServicesResponse<ExamAttemptResultDto>(false, "Exam not found.");
        }

        var isEnrolled = await enrollmentRepository.IsEnrolledAsync(studentId, exam.CourseId);
        if (!isEnrolled)
        {
            return new ServicesResponse<ExamAttemptResultDto>(false, "You are not enrolled in this course.");
        }

        var attempt = new ExamAttempt
        {
            ExamId = dto.ExamId,
            StudentId = studentId,
            StartedAt = DateTime.UtcNow
        };

        await examAttemptRepository.CreateAsync(attempt);

        return new ServicesResponse<ExamAttemptResultDto>(true, "Exam attempt started.", attempt.ExamAttemptToResultMapper());
    }

    // بيسلّم إجابات المحاولة، بيصحح تلقائيًا، وبيحسب النسبة المئوية وهل نجح ولا لأ
    public async Task<ServicesResponse<ExamAttemptResultDto>> SubmitAttemptAsync(int studentId, SubmitExamAttemptDto dto)
    {
        var attempt = await examAttemptRepository.GetByIdAsync(dto.AttemptId);
        if (attempt is null)
        {
            return new ServicesResponse<ExamAttemptResultDto>(false, "Attempt not found.");
        }

        if (attempt.StudentId != studentId)
        {
            return new ServicesResponse<ExamAttemptResultDto>(false, "This attempt does not belong to you.");
        }

        if (attempt.CompletedAt.HasValue)
        {
            return new ServicesResponse<ExamAttemptResultDto>(false, "This attempt was already submitted.");
        }

        var exam = await examRepository.GetExamWithQuestionsAsync(attempt.ExamId);
        if (exam is null)
        {
            return new ServicesResponse<ExamAttemptResultDto>(false, "Exam not found.");
        }

        double totalPoints = exam.Questions.Sum(q => q.Points);
        double earnedPoints = 0;

        foreach (var submitted in dto.Answers)
        {
            var question = exam.Questions.FirstOrDefault(q => q.Id == submitted.QuestionId);
            if (question is null) continue;

            var chosenAnswer = question.Answers.FirstOrDefault(a => a.Id == submitted.AnswerId);
            if (chosenAnswer is { IsCorrect: true })
            {
                earnedPoints += question.Points;
            }

            attempt.SelectedAnswers.Add(new ExamAttemptAnswer
            {
                AttemptId = attempt.Id,
                QuestionId = submitted.QuestionId,
                AnswerId = submitted.AnswerId
            });
        }

        var scorePercentage = totalPoints == 0 ? 0 : Math.Round(earnedPoints / totalPoints * 100, 2);

        attempt.Score = scorePercentage;
        attempt.Passed = scorePercentage >= exam.PassingScore;
        attempt.CompletedAt = DateTime.UtcNow;

        await examAttemptRepository.UpdateAsync(attempt);

        var message = attempt.Passed ? "You passed the exam." : "You did not pass the exam.";
        return new ServicesResponse<ExamAttemptResultDto>(true, message, attempt.ExamAttemptToResultMapper());
    }


    public async Task<ServicesResponse<IEnumerable<ExamAttempt>>> GetAttemptsByStudentAsync(int studentId, int examId)
    {
        var attempts = await examAttemptRepository.GetAttemptsByStudentAsync(studentId, examId);
        if (attempts == null || !attempts.Any())
        {
            return new ServicesResponse<IEnumerable<ExamAttempt>>(false, "No attempts found for the given student id and exam id.", attempts);
        }
        return new ServicesResponse<IEnumerable<ExamAttempt>>(true, "Attempts found for the given student id and exam id.", attempts);
    }

    public async Task<ServicesResponse<int>> GetAttemptsCountAsync(int studentId, int examId)
    {
        var count = await examAttemptRepository.GetAttemptsCountAsync(studentId, examId);
        if (count == 0)
        {
            return new ServicesResponse<int>(false, "No attempts found for the given student id and exam id.", count);
        }
        return new ServicesResponse<int>(true, "Attempt count found for the given student id and exam id.", count);
    }

    public async Task<ServicesResponse<ExamAttempt?>> GetAttemptWithAnswersAsync(int attemptId)
    {
        var attempt = await examAttemptRepository.GetAttemptWithAnswersAsync(attemptId);
        if (attempt == null)
        {
            return new ServicesResponse<ExamAttempt?>(false, "No attempt found for the given attempt id.", attempt);
        }
        return new ServicesResponse<ExamAttempt?>(true, "Attempt found for the given attempt id.", attempt);
    }

    public async Task<ServicesResponse<ExamAttempt?>> GetLatestAttemptAsync(int studentId, int examId)
    {
        var attempt = await examAttemptRepository.GetLatestAttemptAsync(studentId, examId);
        if (attempt == null)
        {
            return new ServicesResponse<ExamAttempt?>(false, "No attempt found for the given student id and exam id.", attempt);
        }
        return new ServicesResponse<ExamAttempt?>(true, "Attempt found for the given student id and exam id.", attempt);
    }

    public async Task<ServicesResponse<bool>> HasPassedAsync(int studentId, int examId)
    {
        var passed = await examAttemptRepository.HasPassedAsync(studentId, examId);
        if (!passed)
        {
            return new ServicesResponse<bool>(false, "No attempt found for the given student id and exam id.");
        }
        return new ServicesResponse<bool>(true, "Attempt found for the given student id and exam id.");
    }
}