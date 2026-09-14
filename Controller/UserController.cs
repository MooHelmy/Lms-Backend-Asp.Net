public class UserController(IUserServices userServices) : BaseApiController
{
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await userServices.GetByEmailAsync(email);
        return HandleResponse(result);
    }
    public async Task<IActionResult> EmailExists(string email)
    {
        var result = await userServices.EmailExistsAsync(email);
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetInstructors()
    {
        var result = await userServices.GetInstructorsAsync();
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetStudents()
    {
        var result = await userServices.GetStudentsAsync();
        return HandleResponse(result);
    }
    public async Task<IActionResult> Deactivate(int userId)
    {
        var result = await userServices.DeactivateAsync(userId);
        return HandleResponse(result);
    }
}