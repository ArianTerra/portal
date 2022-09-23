using EducationPortal.BusinessLogic.DTO;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace EducationPortal.BusinessLogic.Validators.FluentValidation;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    private readonly IConfiguration _configuration;

    public UserLoginDtoValidator(IConfiguration configuration)
    {
        _configuration = configuration;

        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(int.Parse(_configuration["User:Password:MinSize"]))
            .MaximumLength(int.Parse(_configuration["User:Password:MaxSize"]));
    }
}