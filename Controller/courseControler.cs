using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/courses")]
public class CourseController(ICourseServices courseServices) : ControllerBase
{


}