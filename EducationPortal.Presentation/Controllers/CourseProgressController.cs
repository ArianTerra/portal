using System.Security.Claims;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers;

public class CourseProgressController : Controller
{
    private readonly ICourseProgressService _courseProgressService;

    public CourseProgressController(ICourseProgressService courseProgressService)
    {
        _courseProgressService = courseProgressService;
    }

    public async Task<IActionResult> Subscribe(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await _courseProgressService.SubscribeToCourseAsync(userId, id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return Ok();
    }
}