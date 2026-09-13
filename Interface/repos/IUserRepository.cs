using LMS.Domain.Entities;

public interface IUserRepository : IGeneric<User>
{

    Task<User?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    Task<IEnumerable<User>> GetInstructorsAsync();
    Task<IEnumerable<User>> GetStudentsAsync();
    Task DeactivateAsync(int userId);
}