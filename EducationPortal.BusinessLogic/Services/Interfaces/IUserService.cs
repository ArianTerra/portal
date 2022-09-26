using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface IUserService
{
    Task<Result> RegisterUserAsync(UserRegisterDto dto);

    Task<Result> LoginUserAsync(UserLoginDto dto);

    Task<Result> LogoutUserAsync();

    Task<Result<UserAccountDto>> GetUserInfo(Guid userId);

    Task<Result<IEnumerable<CourseDto>>> GetUserCourses(Guid userId);

    Task<Result<IEnumerable<SkillProgressDto>>> GetUserSkills(Guid userId);
}