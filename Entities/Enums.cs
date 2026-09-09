namespace LMS.Domain.Entities
{
    public enum UserRole
    {
        Student = 1,
        Instructor = 2,
        Admin = 3
    }

    public enum LessonContentType
    {
        Video = 1,
        Pdf = 2,
        Article = 3,
        Quiz = 4
    }

    public enum EnrollmentStatus
    {
        Active = 1,
        Completed = 2,
        Cancelled = 3
    }

    public enum SubscriptionStatus
    {
        Active = 1,
        Expired = 2,
        Cancelled = 3
    }

    public enum NotificationType
    {
        Enrollment = 1,
        NewLesson = 2,
        ExamResult = 3,
        CourseCompleted = 4,
        SubscriptionExpiring = 5,
        General = 6
    }
}
