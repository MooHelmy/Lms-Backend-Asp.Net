using System.Text;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public static class ServicesContainer
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            SqlOption =>
             {
                 SqlOption.MigrationsAssembly(typeof(ServicesContainer).Assembly.FullName);
                 SqlOption.EnableRetryOnFailure();
             }
            ),
             ServiceLifetime.Scoped

             );

        // Identity
        services.AddIdentity<User, IdentityRole>(options =>
        {
            // Password rules
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            // Lockout after failed attempts
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.AllowedForNewUsers = true;
            // User rules
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;   // خليها true وقت الإنتاج
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
        // JWT Authentication
        var jwtSettings = configuration.GetSection("Jwt");
        var secretKey = jwtSettings["SigningKey"]!;

        services.AddAuthentication(options =>
          {
              options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = jwtSettings["Issuer"],
                  ValidAudience = jwtSettings["Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
              };
          });
        services.AddAuthorization();
        // Repositories
        // Repositories (للـ Entities البسيطة بس)
        services.AddScoped<IGeneric<Course>, GenericRepository<Course>>();
        services.AddScoped<IGeneric<Section>, GenericRepository<Section>>();
        services.AddScoped<IGeneric<Lesson>, GenericRepository<Lesson>>();
        services.AddScoped<IGeneric<Enrollment>, GenericRepository<Enrollment>>();
        services.AddScoped<IGeneric<LessonProgress>, GenericRepository<LessonProgress>>();
        services.AddScoped<IGeneric<Exam>, GenericRepository<Exam>>();
        services.AddScoped<IGeneric<Question>, GenericRepository<Question>>();
        services.AddScoped<IGeneric<Answer>, GenericRepository<Answer>>();
        services.AddScoped<IGeneric<ExamAttempt>, GenericRepository<ExamAttempt>>();
        services.AddScoped<IGeneric<ExamAttemptAnswer>, GenericRepository<ExamAttemptAnswer>>();
        services.AddScoped<IGeneric<Certificate>, GenericRepository<Certificate>>();
        services.AddScoped<IGeneric<SubscriptionPlan>, GenericRepository<SubscriptionPlan>>();
        services.AddScoped<IGeneric<Subscription>, GenericRepository<Subscription>>();
        services.AddScoped<IGeneric<Review>, GenericRepository<Review>>();
        services.AddScoped<IGeneric<User>, GenericRepository<User>>();
        services.AddScoped<IGeneric<Notification>, GenericRepository<Notification>>();

        // Repositories
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IProgressRepository, ProgressRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<ICertificateRepository, CertificateRepository>();
        services.AddScoped<IExamAttemptRepository, ExamAttemptRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISectionRepository, SectionRepository>();

        // Services
        services.AddScoped<ICourseServices, CourseServices>();
        services.AddScoped<IEnrollmentServices, EnrollmentServices>();
        services.AddScoped<ILessonServices, LessonServices>();
        services.AddScoped<IProgressServices, ProgressServices>();
        services.AddScoped<IExamServices, ExamServices>();
        services.AddScoped<IQuestionServices, QuestionServices>();
        services.AddScoped<IReviewServices, ReviewServices>();
        services.AddScoped<INotificationServices, NotificationServices>();
        services.AddScoped<ISubscriptionServices, SubscriptionServices>();
        services.AddScoped<ICertificateServices, CertificateServices>();
        services.AddScoped<IExamAttemptServices, ExamAttemptServices>();
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<ISectionServices, SectionServices>();
        services.AddScoped<ITokenService, TokenService>();






        return services;
    }
}