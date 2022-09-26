using System.Security.Claims;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services;
using EducationPortal.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers;

[Authorize]
public class CourseProgressController : Controller
{
    private readonly ICourseProgressService _courseProgressService;

    private readonly IMaterialProgressService _materialProgressService;

    public CourseProgressController(ICourseProgressService courseProgressService, IMaterialProgressService materialProgressService)
    {
        _courseProgressService = courseProgressService;
        _materialProgressService = materialProgressService;
    }

    public async Task<IActionResult> Subscribe(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await _courseProgressService.SubscribeToCourseAsync(userId, id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }

    public async Task<IActionResult> Study(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await _courseProgressService.GetCourseProgress(userId, id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return View(result.Value);
    }

    public async Task<IActionResult> StudyMaterial(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await _materialProgressService.SetProgressAsync(id, userId, 100);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }
}