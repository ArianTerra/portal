using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.DataAccess.DomainModels;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly IValidator<UserRegisterDto> _registerDtoValidator;

    private readonly IValidator<UserLoginDto> _loginDtoValidator;

    public UserService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IValidator<UserRegisterDto> registerDtoValidator,
        IValidator<UserLoginDto> loginDtoValidator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _registerDtoValidator = registerDtoValidator;
        _loginDtoValidator = loginDtoValidator;
    }

    public async Task<Result> RegisterUserAsync(UserRegisterDto dto)
    {
        if (dto == null)
        {
            return Result.Fail(new BadRequestError("UserRegisterDto is null"));
        }

        var user = new ApplicationUser()
        {
            UserName = dto.UserName,
            Email = dto.Email
        };

        var validationResult = _registerDtoValidator.Validate(dto);

        if (await _userManager.FindByNameAsync(dto.UserName) != null)
        {
            validationResult.Errors.Add(new ValidationFailure("UserName", "Username already exists"));
        }

        if (await _userManager.FindByEmailAsync(dto.Email) != null)
        {
            validationResult.Errors.Add(new ValidationFailure("Email", "Email already exists"));
        }

        if (!validationResult.IsValid)
        {
            return Result.Fail(new ValidationError(validationResult));
        }

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return Result.Fail(new InternalServerError(result.Errors.FirstOrDefault()?.Description));
        }

        var createdUser = await _userManager.FindByNameAsync(user.UserName);

        if (createdUser == null)
        {
            return Result.Fail(new InternalServerError("User was not created"));
        }

        return Result.Ok();
    }

    public async Task<Result> LoginUserAsync(UserLoginDto dto)
    {
        if (dto == null)
        {
            return Result.Fail(new BadRequestError("UserLoginDto is null"));
        }

        var validationResult = _loginDtoValidator.Validate(dto);

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);

        if (user == null)
        {
            validationResult.Errors.Add(new ValidationFailure("UserName", "User does not exist"));
            return Result.Fail(new ValidationError(validationResult));
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!isPasswordCorrect)
        {
            validationResult.Errors.Add(new ValidationFailure("Password", "Password is incorrect"));
        }

        if (!validationResult.IsValid)
        {
            return Result.Fail(new ValidationError(validationResult));
        }

        await _signInManager.SignInAsync(user, true);

        return Result.Ok();
    }

    public async Task<Result> LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();

        return Result.Ok();
    }
}