using System.Linq.Expressions;
using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.Repositories;
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

    private readonly IGenericRepository<ApplicationUser> _userRepository;

    private readonly IGenericRepository<Course> _courseRepository;

    private readonly IGenericRepository<Skill> _skillRepository;

    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IValidator<UserRegisterDto> registerDtoValidator,
        IValidator<UserLoginDto> loginDtoValidator,
        IGenericRepository<ApplicationUser> userRepository,
        IGenericRepository<Course> courseRepository,
        IMapper mapper,
        IGenericRepository<Skill> skillRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _registerDtoValidator = registerDtoValidator;
        _loginDtoValidator = loginDtoValidator;
        _userRepository = userRepository;
        _courseRepository = courseRepository;
        _mapper = mapper;
        _skillRepository = skillRepository;
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
        if (!validationResult.IsValid)
        {
            return Result.Fail(new ValidationError(validationResult));
        }

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
        if (!validationResult.IsValid)
        {
            return Result.Fail(new ValidationError(validationResult));
        }

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

    public async Task<Result<UserAccountDto>> GetUserInfo(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return Result.Fail(new NotFoundError(userId));
        }

        var info = new UserAccountDto()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };

        return Result.Ok(info);
    }

    public async Task<Result<IEnumerable<CourseDto>>> GetUserCourses(Guid userId)
    {
        var user = await _userRepository.FindFirstAsync(
            x => x.Id == userId,
            includes: new Expression<Func<ApplicationUser, object>>[]
            {
                x => x.CourseProgresses
            });

        var coursesDtos = new List<CourseDto>();

        foreach (var progress in user.CourseProgresses)
        {
            var course = await _courseRepository.FindFirstAsync(x => x.Id == progress.CourseId);
            coursesDtos.Add(_mapper.Map<CourseDto>(course));
        }

        return coursesDtos;
    }

    public async Task<Result<IEnumerable<SkillProgressDto>>> GetUserSkills(Guid userId)
    {
        var user = await _userRepository.FindFirstAsync(
            x => x.Id == userId,
            includes: new Expression<Func<ApplicationUser, object>>[]
            {
                x => x.SkillProgresses
            });

        var skillProgressDtos = new List<SkillProgressDto>();

        foreach (var progress in user.SkillProgresses)
        {
            var skill = await _skillRepository.FindFirstAsync(x => x.Id == progress.SkillId);

            skillProgressDtos.Add(new SkillProgressDto
            {
                SkillId = skill.Id,
                Name = skill.Name,
                Level = progress.Level
            });
        }

        return skillProgressDtos;
    }
}