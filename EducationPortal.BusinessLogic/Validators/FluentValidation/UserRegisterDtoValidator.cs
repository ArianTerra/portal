using EducationPortal.BusinessLogic.DTO;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace EducationPortal.BusinessLogic.Validators.FluentValidation;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    private readonly IConfiguration _configuration;

    public UserRegisterDtoValidator(IConfiguration configuration)
    {
        _configuration = configuration;

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(int.Parse(_configuration["User:Username:MaxSize"]));

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(int.Parse(_configuration["User:Password:MinSize"]))
            .MaximumLength(int.Parse(_configuration["User:Password:MaxSize"]));
    }
}