using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface IUserService
{
    Task<Result> RegisterUserAsync(UserRegisterDto dto);

    Task<Result> LoginUserAsync(UserLoginDto dto);

    Task<Result> LogoutUserAsync();
}