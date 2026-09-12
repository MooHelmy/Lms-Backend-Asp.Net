using LMS.Domain.Entities;

public class UserServices(IUserRepository userRepository) : IUserServices
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
}