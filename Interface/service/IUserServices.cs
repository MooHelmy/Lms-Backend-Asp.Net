using LMS.Domain.Entities;

public interface IUserServices
{

    Task<ServicesResponse<User?>> GetByEmailAsync(string email);
    Task<ServicesResponse<bool>> EmailExistsAsync(string email);
    Task<ServicesResponse<IEnumerable<User>>> GetInstructorsAsync();
    Task<ServicesResponse<IEnumerable<User>>> GetStudentsAsync();
    Task<ServicesResponse<bool>> DeactivateAsync(int userId);
}