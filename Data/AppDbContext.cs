using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ================== User ==================
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.Name).IsRequired().HasMaxLength(150);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
            entity.Property(u => u.PasswordHash).IsRequired();

            // مفيش اتنين يقدروا يسجلوا بنفس الإيميل
            entity.HasIndex(u => u.Email).IsUnique();
        });

        // ================== Category ==================
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(c => c.Name).IsUnique();
        });

        // ================== Course ==================
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(c => c.Title).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Description).IsRequired();
            entity.Property(c => c.Price).HasPrecision(18, 2);

            // Restrict عشان منمنعش حذف Instructor لو ليه كورسات مربوطة بيه
            entity.HasOne(c => c.Instructor)
                  .WithMany(u => u.CoursesCreated)
                  .HasForeignKey(c => c.InstructorId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Category)
                  .WithMany(cat => cat.Courses)
                  .HasForeignKey(c => c.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ================== Section ==================
        modelBuilder.Entity<Section>(entity =>
        {
            entity.Property(s => s.Title).IsRequired().HasMaxLength(200);

            // لو الكورس اتحذف، الـ Sections بتاعته تتحذف معاه (ملكية مباشرة، مفيش تعارض)
            entity.HasOne(s => s.Course)
                  .WithMany(c => c.Sections)
                  .HasForeignKey(s => s.CourseId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ================== Lesson ==================
        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.Property(l => l.Title).IsRequired().HasMaxLength(200);

            entity.HasOne(l => l.Section)
                  .WithMany(s => s.Lessons)
                  .HasForeignKey(l => l.SectionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ================== Enrollment ==================
        modelBuilder.Entity<Enrollment>(entity =>
        {
            // الطالب الواحد يقدر يسجل في نفس الكورس مرة واحدة بس
            entity.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();

            entity.HasOne(e => e.Student)
                  .WithMany(u => u.Enrollments)
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Course)
                  .WithMany(c => c.Enrollments)
                  .HasForeignKey(e => e.CourseId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ================== LessonProgress ==================
        modelBuilder.Entity<LessonProgress>(entity =>
        {
            // سجل تقدم واحد بس لكل طالب في كل درس
            entity.HasIndex(p => new { p.StudentId, p.LessonId }).IsUnique();

            entity.HasOne(p => p.Student)
                  .WithMany()
                  .HasForeignKey(p => p.StudentId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Lesson)
                  .WithMany(l => l.ProgressRecords)
                  .HasForeignKey(p => p.LessonId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ================== Exam / Question / Answer ==================
        modelBuilder.Entity<Exam>(entity =>
        {
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);

            entity.HasOne(e => e.Course)
                  .WithMany(c => c.Exams)
                  .HasForeignKey(e => e.CourseId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(q => q.Text).IsRequired();

            entity.HasOne(q => q.Exam)
                  .WithMany(e => e.Questions)
                  .HasForeignKey(q => q.ExamId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.Property(a => a.Text).IsRequired().HasMaxLength(500);

            entity.HasOne(a => a.Question)
                  .WithMany(q => q.Answers)
                  .HasForeignKey(a => a.QuestionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ================== ExamAttempt / ExamAttemptAnswer ==================
        modelBuilder.Entity<ExamAttempt>(entity =>
        {
            entity.HasOne(a => a.Exam)
                  .WithMany(e => e.Attempts)
                  .HasForeignKey(a => a.ExamId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Student)
                  .WithMany()
                  .HasForeignKey(a => a.StudentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ExamAttemptAnswer>(entity =>
        {
            entity.HasOne(a => a.Attempt)
                  .WithMany(at => at.SelectedAnswers)
                  .HasForeignKey(a => a.AttemptId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Question)
                  .WithMany()
                  .HasForeignKey(a => a.QuestionId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.Answer)
                  .WithMany()
                  .HasForeignKey(a => a.AnswerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ================== Certificate ==================
        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.Property(c => c.CertificateNumber).IsRequired().HasMaxLength(50);

            // رقم الشهادة لازم يكون فريد عشان نستخدمه في التحقق (Verify)
            entity.HasIndex(c => c.CertificateNumber).IsUnique();

            entity.HasOne(c => c.Student)
                  .WithMany(u => u.Certificates)
                  .HasForeignKey(c => c.StudentId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Course)
                  .WithMany(co => co.Certificates)
                  .HasForeignKey(c => c.CourseId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ================== SubscriptionPlan / Subscription ==================
        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasOne(s => s.User)
                  .WithMany(u => u.Subscriptions)
                  .HasForeignKey(s => s.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Plan)
                  .WithMany(p => p.Subscriptions)
                  .HasForeignKey(s => s.PlanId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ================== Review ==================
        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(r => r.Comment).HasMaxLength(1000);

            // الطالب يقيّم الكورس مرة واحدة بس
            entity.HasIndex(r => new { r.StudentId, r.CourseId }).IsUnique();

            entity.HasOne(r => r.Student)
                  .WithMany(u => u.Reviews)
                  .HasForeignKey(r => r.StudentId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Course)
                  .WithMany(c => c.Reviews)
                  .HasForeignKey(r => r.CourseId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ================== Notification ==================
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(n => n.Title).IsRequired().HasMaxLength(150);
            entity.Property(n => n.Message).IsRequired().HasMaxLength(1000);

            // مسار واحد بس من User للـ Notification، فمفيش تعارض في الـ Cascade
            entity.HasOne(n => n.User)
                  .WithMany(u => u.Notifications)
                  .HasForeignKey(n => n.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });



    }
    // ---------- DbSets ----------
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<LessonProgress> LessonProgresses { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<ExamAttempt> ExamAttempts { get; set; }
    public DbSet<ExamAttemptAnswer> ExamAttemptAnswers { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Notification> Notifications { get; set; }

}