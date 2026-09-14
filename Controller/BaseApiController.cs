using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers;

// كل الـ Controllers هترث من هنا عشان تاخد ميثودز مشتركة (قراءة الـ UserId، وتوحيد شكل الرد)
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    // بيقرا الـ UserId من الـ JWT Claims بتاعة اليوزر المسجل دخوله حاليًا
    protected int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // بيحوّل ServicesResponse<T> لرد HTTP مناسب: 200 لو نجح، 400 لو فشل
    protected IActionResult HandleResponse<T>(ServicesResponse<T> response)
    {
        if (!response.Success)
        {
            return BadRequest(new { response.Message });
        }

        return Ok(new { response.Message, Data = response.Data });
    }

    // نفس الفكرة لكن للنسخة الغير Generic (اللي بترجع Success/Message بس من غير Data)
    protected IActionResult HandleResponse(ServicesResponse response)
    {
        if (!response.Success)
        {
            return BadRequest(new { response.Message });
        }

        return Ok(new { response.Message });
    }
}
