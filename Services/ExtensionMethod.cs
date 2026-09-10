
using LMS.Application.DTOs.Certificates;
using LMS.Application.DTOs.Common;
using LMS.Application.DTOs.Courses;
using LMS.Application.DTOs.Enrollments;
using LMS.Application.DTOs.ExamAttempts;
using LMS.Application.DTOs.Exams;
using LMS.Application.DTOs.Lessons;
using LMS.Application.DTOs.Notifications;
using LMS.Application.DTOs.Progress;
using LMS.Application.DTOs.Reviews;
using LMS.Application.DTOs.Sections;
using LMS.Application.DTOs.Subscriptions;
using LMS.Application.DTOs.Users;
using LMS.Domain.Entities;

namespace LMS.Application.Mappers
{
    public static class MapperExtensions
    {



        // =========================================================
        // COURSE MAPPERS
        // =========================================================

        public static Course CourseCreateToEntityMapper(
            this CourseCreateDto dto,
            int instructorId)
        {
            return new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                InstructorId = instructorId,
                IsPublished = false
            };
        }

        public static void CourseUpdateMapper(
            this Course course,
            CourseUpdateDto dto)
        {
            course.Title = dto.Title;
            course.Description = dto.Description;
            course.Price = dto.Price;
            course.CategoryId = dto.CategoryId;
            course.IsPublished = dto.IsPublished;
        }

        public static CourseListItemDto CourseToListItemMapper(
            this Course course)
        {
            return new CourseListItemDto
            {
                Id = course.Id,
                Title = course.Title,
                ThumbnailUrl = course.ThumbnailUrl,
                Price = course.Price,

                InstructorName = course.Instructor?.Name ?? string.Empty,
                CategoryName = course.Category?.Name ?? string.Empty,

                AverageRating = course.Reviews.Any()
                    ? Math.Round(course.Reviews.Average(r => r.Rating), 2)
                    : 0,

                ReviewsCount = course.Reviews.Count,
                EnrollmentsCount = course.Enrollments.Count
            };
        }

        public static CourseDetailsDto CourseToDetailsMapper(
            this Course course,
            int? currentStudentId = null)
        {
            var lessons = course.Sections
                .SelectMany(s => s.Lessons)
                .ToList();

            return new CourseDetailsDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                ThumbnailUrl = course.ThumbnailUrl,
                Price = course.Price,
                IsPublished = course.IsPublished,

                InstructorId = course.InstructorId,
                InstructorName = course.Instructor?.Name ?? string.Empty,

                CategoryName = course.Category?.Name ?? string.Empty,

                AverageRating = course.Reviews.Any()
                    ? Math.Round(course.Reviews.Average(r => r.Rating), 2)
                    : 0,

                ReviewsCount = course.Reviews.Count,

                TotalLessons = lessons.Count,

                TotalDurationInSeconds =
                    lessons.Sum(l => l.DurationInSeconds),

                Sections = course.Sections
                    .OrderBy(s => s.Order)
                    .Select(s => s.SectionToSummaryMapper(currentStudentId))
                    .ToList()
            };
        }


        // =========================================================
        // SECTION MAPPERS
        // =========================================================

        public static Section SectionCreateToEntityMapper(
            this SectionCreateDto dto)
        {
            return new Section
            {
                Title = dto.Title,
                Description = dto.Description,
                Order = dto.Order,
                CourseId = dto.CourseId
            };
        }

        public static void SectionUpdateMapper(
            this Section section,
            SectionUpdateDto dto)
        {
            section.Title = dto.Title;
            section.Description = dto.Description;
            section.Order = dto.Order;
        }

        public static SectionResponseDto SectionToResponseMapper(
            this Section section)
        {
            return new SectionResponseDto
            {
                Id = section.Id,
                Title = section.Title,
                Description = section.Description,
                Order = section.Order,
                CourseId = section.CourseId
            };
        }

        public static SectionSummaryDto SectionToSummaryMapper(
            this Section section,
            int? currentStudentId = null)
        {
            return new SectionSummaryDto
            {
                Id = section.Id,
                Title = section.Title,
                Order = section.Order,

                Lessons = section.Lessons
                    .OrderBy(l => l.Order)
                    .Select(l => l.LessonToSummaryMapper(currentStudentId))
                    .ToList()
            };
        }


        // =========================================================
        // LESSON MAPPERS
        // =========================================================

        public static Lesson LessonCreateToEntityMapper(
            this LessonCreateDto dto)
        {
            return new Lesson
            {
                Title = dto.Title,
                Description = dto.Description,
                ContentType = dto.ContentType,
                VideoUrl = dto.VideoUrl,
                DurationInSeconds = dto.DurationInSeconds,
                Order = dto.Order,
                SectionId = dto.SectionId
            };
        }

        public static void LessonUpdateMapper(
            this Lesson lesson,
            LessonUpdateDto dto)
        {
            lesson.Title = dto.Title;
            lesson.Description = dto.Description;
            lesson.VideoUrl = dto.VideoUrl;
            lesson.DurationInSeconds = dto.DurationInSeconds;
            lesson.Order = dto.Order;
        }

        public static LessonResponseDto LessonToResponseMapper(
            this Lesson lesson)
        {
            return new LessonResponseDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                ContentType = lesson.ContentType.ToString(),
                VideoUrl = lesson.VideoUrl,
                DurationInSeconds = lesson.DurationInSeconds,
                Order = lesson.Order,
                SectionId = lesson.SectionId
            };
        }

        public static LessonSummaryDto LessonToSummaryMapper(
            this Lesson lesson,
            int? currentStudentId = null)
        {
            var isCompleted = false;

            if (currentStudentId.HasValue)
            {
                isCompleted = lesson.ProgressRecords
                    .Any(p =>
                        p.StudentId == currentStudentId.Value &&
                        p.IsCompleted);
            }

            return new LessonSummaryDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                ContentType = lesson.ContentType.ToString(),
                DurationInSeconds = lesson.DurationInSeconds,
                Order = lesson.Order,
                IsCompleted = isCompleted
            };
        }


        // =========================================================
        // ENROLLMENT MAPPERS
        // =========================================================

        public static Enrollment EnrollmentCreateToEntityMapper(
            this EnrollDto dto,
            int studentId)
        {
            return new Enrollment
            {
                StudentId = studentId,
                CourseId = dto.CourseId,
                Status = EnrollmentStatus.Active
            };
        }

        public static EnrollmentResponseDto EnrollmentToResponseMapper(
            this Enrollment enrollment,
            int progressPercentage = 0)
        {
            return new EnrollmentResponseDto
            {
                Id = enrollment.Id,
                CourseId = enrollment.CourseId,

                CourseTitle = enrollment.Course?.Title
                              ?? string.Empty,

                EnrolledAt = enrollment.EnrolledAt,
                CompletedAt = enrollment.CompletedAt,

                Status = enrollment.Status.ToString(),

                ProgressPercentage = progressPercentage
            };
        }


        // =========================================================
        // PROGRESS MAPPERS
        // =========================================================

        public static LessonProgress LessonProgressCreateMapper(
            this UpdateLessonProgressDto dto,
            int studentId)
        {
            return new LessonProgress
            {
                StudentId = studentId,
                LessonId = dto.LessonId,
                WatchedSeconds = dto.WatchedSeconds,
                IsCompleted = dto.IsCompleted,

                CompletedAt = dto.IsCompleted
                    ? DateTime.UtcNow
                    : null
            };
        }

        public static void LessonProgressUpdateMapper(
            this LessonProgress progress,
            UpdateLessonProgressDto dto)
        {
            progress.WatchedSeconds = dto.WatchedSeconds;

            if (dto.IsCompleted && !progress.IsCompleted)
            {
                progress.IsCompleted = true;
                progress.CompletedAt = DateTime.UtcNow;
            }
        }

        public static CourseProgressDto CourseProgressToDtoMapper(
            this CourseProgressDto progress)
        {
            return new CourseProgressDto
            {
                CourseId = progress.CourseId,
                CompletedLessons = progress.CompletedLessons,
                TotalLessons = progress.TotalLessons,
                ProgressPercentage = progress.ProgressPercentage
            };
        }


        // =========================================================
        // EXAM MAPPERS
        // =========================================================

        public static Exam ExamCreateToEntityMapper(
            this ExamCreateDto dto)
        {
            return new Exam
            {
                Title = dto.Title,
                Description = dto.Description,
                DurationInMinutes = dto.DurationInMinutes,
                PassingScore = dto.PassingScore,
                CourseId = dto.CourseId
            };
        }

        public static void ExamUpdateMapper(
            this Exam exam,
            ExamUpdateDto dto)
        {
            exam.Title = dto.Title;
            exam.Description = dto.Description;
            exam.DurationInMinutes = dto.DurationInMinutes;
            exam.PassingScore = dto.PassingScore;
        }

        public static ExamDetailsDto ExamToDetailsMapper(
            this Exam exam)
        {
            return new ExamDetailsDto
            {
                Id = exam.Id,
                Title = exam.Title,
                Description = exam.Description,
                DurationInMinutes = exam.DurationInMinutes,
                PassingScore = exam.PassingScore,

                Questions = exam.Questions
                    .OrderBy(q => q.Order)
                    .Select(q => q.QuestionToDetailsMapper())
                    .ToList()
            };
        }

        public static ExamForStudentDto ExamToStudentMapper(
            this Exam exam)
        {
            return new ExamForStudentDto
            {
                Id = exam.Id,
                Title = exam.Title,
                DurationInMinutes = exam.DurationInMinutes,

                Questions = exam.Questions
                    .OrderBy(q => q.Order)
                    .Select(q => q.QuestionToStudentMapper())
                    .ToList()
            };
        }


        // =========================================================
        // QUESTION MAPPERS
        // =========================================================

        public static Question QuestionCreateToEntityMapper(
            this QuestionCreateDto dto,
            int examId)
        {
            var question = new Question
            {
                ExamId = examId,
                Text = dto.Text,
                Points = dto.Points,
                Order = dto.Order
            };

            question.Answers = dto.Answers
                .Select(a => a.AnswerCreateToEntityMapper(question.Id))
                .ToList();

            return question;
        }

        public static QuestionDetailsDto QuestionToDetailsMapper(
            this Question question)
        {
            return new QuestionDetailsDto
            {
                Id = question.Id,
                Text = question.Text,
                Points = question.Points,
                Order = question.Order,

                Answers = question.Answers
                    .Select(a => a.AnswerToDetailsMapper())
                    .ToList()
            };
        }

        public static QuestionForStudentDto QuestionToStudentMapper(
            this Question question)
        {
            return new QuestionForStudentDto
            {
                Id = question.Id,
                Text = question.Text,
                Points = question.Points,

                Answers = question.Answers
                    .Select(a => a.AnswerToStudentMapper())
                    .ToList()
            };
        }


        // =========================================================
        // ANSWER MAPPERS
        // =========================================================

        public static Answer AnswerCreateToEntityMapper(
            this AnswerCreateDto dto,
            int questionId = 0)
        {
            return new Answer
            {
                Text = dto.Text,
                IsCorrect = dto.IsCorrect,
                QuestionId = questionId
            };
        }

        public static AnswerDetailsDto AnswerToDetailsMapper(
            this Answer answer)
        {
            return new AnswerDetailsDto
            {
                Id = answer.Id,
                Text = answer.Text,
                IsCorrect = answer.IsCorrect
            };
        }

        public static AnswerForStudentDto AnswerToStudentMapper(
            this Answer answer)
        {
            return new AnswerForStudentDto
            {
                Id = answer.Id,
                Text = answer.Text
            };
        }


        // =========================================================
        // EXAM ATTEMPT MAPPERS
        // =========================================================

        public static ExamAttemptResultDto ExamAttemptToResultMapper(
            this ExamAttempt attempt)
        {
            return new ExamAttemptResultDto
            {
                AttemptId = attempt.Id,
                ExamId = attempt.ExamId,
                Score = attempt.Score,
                Passed = attempt.Passed,
                StartedAt = attempt.StartedAt,
                CompletedAt = attempt.CompletedAt
            };
        }


        // =========================================================
        // REVIEW MAPPERS
        // =========================================================

        public static Review ReviewCreateToEntityMapper(
            this ReviewCreateDto dto,
            int studentId)
        {
            return new Review
            {
                StudentId = studentId,
                CourseId = dto.CourseId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };
        }

        public static ReviewResponseDto ReviewToResponseMapper(
            this Review review)
        {
            return new ReviewResponseDto
            {
                Id = review.Id,
                StudentName = review.Student?.Name ?? string.Empty,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };
        }


        // =========================================================
        // NOTIFICATION MAPPERS
        // =========================================================

        public static Notification NotificationCreateToEntityMapper(
            this CreateNotificationDto dto)
        {
            return new Notification
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Message = dto.Message,
                Type = dto.Type,
                IsRead = false
            };
        }

        public static NotificationResponseDto NotificationToResponseMapper(
            this Notification notification)
        {
            return new NotificationResponseDto
            {
                Id = notification.Id,
                Title = notification.Title,
                Message = notification.Message,
                Type = notification.Type.ToString(),
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt
            };
        }


        // =========================================================
        // CERTIFICATE MAPPERS
        // =========================================================

        public static CertificateResponseDto CertificateToResponseMapper(
            this Certificate certificate)
        {
            return new CertificateResponseDto
            {
                Id = certificate.Id,
                CertificateNumber = certificate.CertificateNumber,

                StudentName =
                    certificate.Student?.Name ?? string.Empty,

                CourseTitle =
                    certificate.Course?.Title ?? string.Empty,

                InstructorName =
                    certificate.Course?.Instructor?.Name ?? string.Empty,

                FileUrl = certificate.FileUrl,
                IssuedAt = certificate.IssuedAt
            };
        }

        public static VerifyCertificateDto CertificateToVerifyMapper(
            this Certificate? certificate)
        {
            return new VerifyCertificateDto
            {
                IsValid = certificate is not null,

                Certificate = certificate is null
                    ? null
                    : certificate.CertificateToResponseMapper()
            };
        }


        // =========================================================
        // SUBSCRIPTION PLAN MAPPERS
        // =========================================================

        public static SubscriptionPlanDto SubscriptionPlanToDtoMapper(
            this SubscriptionPlan plan)
        {
            return new SubscriptionPlanDto
            {
                Id = plan.Id,
                Name = plan.Name,
                Price = plan.Price,
                DurationInDays = plan.DurationInDays
            };
        }


        // =========================================================
        // SUBSCRIPTION MAPPERS
        // =========================================================

        public static SubscriptionResponseDto SubscriptionToResponseMapper(
            this Subscription subscription)
        {
            return new SubscriptionResponseDto
            {
                Id = subscription.Id,

                PlanName =
                    subscription.Plan?.Name ?? string.Empty,

                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate,

                Status = subscription.Status.ToString(),

                IsActive =
                    subscription.Status == SubscriptionStatus.Active &&
                    subscription.EndDate > DateTime.UtcNow
            };
        }


        // =========================================================
        // PAGINATION MAPPERS
        // =========================================================

        public static PagedResult<TDto> ToPagedResult<TSource, TDto>(
            this PagedResult<TSource> source,
            Func<TSource, TDto> mapper)
        {
            return new PagedResult<TDto>
            {
                Items = source.Items.Select(mapper).ToList(),
                Page = source.Page,
                PageSize = source.PageSize,
                TotalCount = source.TotalCount
            };
        }
    }
}