using System.Security.Claims;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.Presentation.ViewModels;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    private readonly ICourseProgressService _courseProgressService;

    public AccountController(IUserService userService, ICourseProgressService courseProgressService)
    {
        _userService = userService;
        _courseProgressService = courseProgressService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var userInfo = await _userService.GetUserInfo(userId);

        var userCourses = await _userService.GetUserCourses(userId);
        var userCourseProgress = new List<CourseProgressDto>();
        foreach (var course in userCourses.Value)
        {
            var progress = await _courseProgressService.GetCourseProgress(userId, course.Id);
            userCourseProgress.Add(progress.Value);
        }

        var userSkills = await _userService.GetUserSkills(userId);

        var viewModel = new AccountViewModel()
        {
            UserAccountDto = userInfo.Value,
            CoursesProgress = userCourseProgress,
            SkillProgress = userSkills.Value
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterDto dto)
    {
        var result = await _userService.RegisterUserAsync(dto);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == (int)ErrorCode.ValidationError)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);

                return View(dto);
            }

            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var result = await _userService.LoginUserAsync(dto);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == (int)ErrorCode.ValidationError)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);

                return View(dto);
            }

            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutUserAsync();

        return Redirect(Request.Headers["Referer"].ToString());
    }
}