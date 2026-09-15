using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class UserServices(IUserRepository userRepository, UserManager<User> userManager,
    SignInManager<User> signInManager,
    RoleManager<IdentityRole> roleManager,
    ITokenService tokenService,
    IConfiguration configuration) : IUserServices
{
    public async Task<ServicesResponse<bool>> DeactivateAsync(int userId)
    {
        await userRepository.DeactivateAsync(userId);
        return new ServicesResponse<bool>(true, "User deactivated.");
    }

    public async Task<ServicesResponse<bool>> EmailExistsAsync(string email)
    {
        var exists = await userRepository.EmailExistsAsync(email);
        return new ServicesResponse<bool>(true, "Email existence check completed.", exists);
    }

    public async Task<ServicesResponse<User?>> GetByEmailAsync(string email)
    {
        var user = await userRepository.GetByEmailAsync(email);
        return new ServicesResponse<User?>(true, "User found.", user);
    }

    public async Task<ServicesResponse<IEnumerable<User>>> GetInstructorsAsync()
    {
        var instructors = await userRepository.GetInstructorsAsync();
        return new ServicesResponse<IEnumerable<User>>(true, "Instructors found.", instructors);
    }

    public async Task<ServicesResponse<IEnumerable<User>>> GetStudentsAsync()
    {
        var students = await userRepository.GetStudentsAsync();
        return new ServicesResponse<IEnumerable<User>>(true, "Students found.", students);
    }
    public async Task<ServicesResponse<AuthResponse>> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            return new ServicesResponse<AuthResponse>(false, "يوجد حساب بهذا الإيميل بالفعل");
        }
        var user = new User
        {
            UserName = dto.Email,
            Email = dto.Email,
            FullName = dto.FullName,
            StreetName = dto.StreetName,
            City = dto.City,
            CreatedAt = DateTime.UtcNow,

        };
        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            return new ServicesResponse<AuthResponse>(false, string.Join(" | ", result.Errors.Select(e => e.Description)));
        }
        await userManager.AddToRoleAsync(user, "User");
        return new ServicesResponse<AuthResponse>(true, "تم إنشاء الحساب بنجاح", new AuthResponse { Success = true, Message = "تم إنشاء الحساب بنجاح" });
    }
    public async Task<ServicesResponse<AuthResponse>> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            return new ServicesResponse<AuthResponse>(false, "البيانات غير صحيحة");
        }


        var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
        if (result.IsLockedOut)
        {
            return new ServicesResponse<AuthResponse>(false, "الحساب مقفول مؤقتًا بسبب محاولات دخول فاشلة كثيرة", new AuthResponse { Success = false, Message = "الحساب مقفول مؤقتًا بسبب محاولات دخول فاشلة كثيرة" });
        }

        if (!result.Succeeded)
        {
            return new ServicesResponse<AuthResponse>(false, "البيانات غير صحيحة", new AuthResponse { Success = false, Message = "البيانات غير صحيحة" });
        }

        var roles = await userManager.GetRolesAsync(user);
        var accessToken = tokenService.CreateToken(user, roles);
        var refreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await userManager.UpdateAsync(user);

        return new ServicesResponse<AuthResponse>(true, "تم تسجيل الدخول بنجاح", new AuthResponse
        {
            Success = true,
            Message = "تم تسجيل الدخول بنجاح",
            Token = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(double.Parse(configuration["Jwt:ExpiryMinutes"]!))
        });
    }
    public async Task<ServicesResponse> LogoutAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ServicesResponse(false, "User not found");
        }
        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        await userManager.UpdateAsync(user);
        return new ServicesResponse(true, "تم تسجيل الخروج بنجاح");
    }
    public async Task<ServicesResponse<AuthResponse>> RefreshTokenAsync(RefreshTokenDto dto)
    {
        var principal = GetPrincipalFromExpiredToken(dto.AccessToken);
        var userId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
        var user = await userManager.FindByIdAsync(userId!);
        if (user == null || user.RefreshToken != dto.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            return new ServicesResponse<AuthResponse>(false, "Refresh Token غير صالح أو منتهي", new AuthResponse { Success = false, Message = "Refresh Token غير صالح أو منتهي" });
        }


        var roles = await userManager.GetRolesAsync(user);
        var newAccessToken = tokenService.CreateToken(user, roles);
        var newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await userManager.UpdateAsync(user);

        return new ServicesResponse<AuthResponse>(true, "تم تحديث كلمة المرور بنجاح", new AuthResponse
        {
            Success = true,
            Token = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(double.Parse(configuration["Jwt:ExpiryMinutes"]!))
        });


    }
    public async Task<ServicesResponse> ForgotPasswordAsync(ForgotPasswordDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        const string genericMessage = "لو الإيميل ده مسجل عندنا، هيوصلك رابط استرجاع الباسورد";

        // ملحوظة أمان: بنرجع نفس الرسالة سواء الإيميل مسجل دخول أو غير مسجل دخول
        if (user == null)
        {
            return new ServicesResponse(true, genericMessage);
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = $"https://yourapp.com/reset-password?email={dto.Email}&token={Uri.EscapeDataString(token)}";
        // await emailService.SendAsync(dto.Email, "Password Reset", resetLink);

        return new ServicesResponse(true, genericMessage);
        // await emailService.SendAsync(dto.Email, "Password Reset", resetLink);
    }
    public async Task<ServicesResponse> ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            return new ServicesResponse(false, "طلب غير صالح");
        }

        var result = await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        if (!result.Succeeded)
        {
            return new ServicesResponse(false, string.Join(" | ", result.Errors.Select(e => e.Description)));
        }

        return new ServicesResponse(true, "تم تغيير كلمة المرور بنجاح");
    }
    public async Task<ServicesResponse> ConfirmEmailAsync(string userId, string token)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ServicesResponse(false, "مستخدم غير موجود");
        }

        var result = await userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return new ServicesResponse(false, "فشل تأكيد البريد الإلكتروني، الرابط غير صالح أو منتهي");
        }

        return new ServicesResponse(true, "تم تأكيد البريد الإلكتروني بنجاح");

    }
    public async Task<ServicesResponse> ChangePasswordAsync(string userId, ChangePasswordDto dto)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ServicesResponse(false, "User not found");
        }

        var result = await userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
        if (!result.Succeeded)
        {
            return new ServicesResponse(false, string.Join(" | ", result.Errors.Select(e => e.Description)));
        }

        return new ServicesResponse(true, "تم تغيير كلمة المرور بنجاح");
    }
    public async Task<GetProfile> GetProfileAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new ItemNotFoundException($"User with id {userId} was not found");
        }
        return new GetProfile
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            StreetName = user.StreetName,
            City = user.City,
            EmailConfirmed = user.EmailConfirmed

        };


    }
    public async Task<ServicesResponse<IEnumerable<GetProfile>>> GetAllUsersAsync()
    {
        var users = userManager.Users.ToList();

        return new ServicesResponse<IEnumerable<GetProfile>>(true, "تم جلب جميع المستخدمين", users.Select(u => new GetProfile
        {
            Id = u.Id,
            Email = u.Email,
            FullName = u.FullName,
            StreetName = u.StreetName,
            City = u.City,
            EmailConfirmed = u.EmailConfirmed
        }));
    }
    public async Task<ServicesResponse> UpdateProfileAsync(string userId, UpdateProfileDto dto)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ServicesResponse(false, "User not found");
        }
        if (dto.FullName != null) user.FullName = dto.FullName;
        if (dto.StreetName != null) user.StreetName = dto.StreetName;
        if (dto.City != null) user.City = dto.City;

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return new ServicesResponse(false, string.Join(" | ", result.Errors.Select(e => e.Description)));
        }

        return new ServicesResponse(true, "تم تحديث البيانات بنجاح");
    }
    public async Task<ServicesResponse> AssignRoleAsync(string userId, string roleName)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ServicesResponse(false, "مستخدم غير موجود");
        }

        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
        var result = await userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded)
        {
            return new ServicesResponse(false, string.Join(" | ", result.Errors.Select(e => e.Description)));
        }

        return new ServicesResponse(true, $"تم إعطاء الدور {roleName} للمستخدم بنجاح");
    }
    public async Task<ServicesResponse> RemoveRoleAsync(string userId, string roleName)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ServicesResponse(false, "مستخدم غير موجود");
        }
        await userManager.RemoveFromRoleAsync(user, roleName);
        return new ServicesResponse(true, $"تم إزالة الدور {roleName}");
    }
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SigningKey"]!)),
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = false // الفرق هنا: مش بنتحقق من انتهاء الصلاحية
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtToken ||
            !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("توكن غير صالح");
        }

        return principal;
    }



    Task<ServicesResponse<GetProfile>> IUserServices.GetProfileAsync(string userId)
    {
        throw new NotImplementedException();
    }
}