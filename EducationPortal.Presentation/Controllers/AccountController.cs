using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
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