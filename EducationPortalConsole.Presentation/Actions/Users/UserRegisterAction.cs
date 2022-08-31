using EducationPortalConsole.BusinessLogic.Helpers.Hasher;
using EducationPortalConsole.BusinessLogic.Validators;
using EducationPortalConsole.Core.Entities;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Users;

public class UserRegisterAction : Action
{
    public UserRegisterAction()
    {
        Name = "User Registration";
    }

    public override void Run()
    {
        base.Run();

        var userService = Configuration.Instance.UserService;

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your [green]name[/]:")
                .Validate(x =>
                    userService.GetUserByName(x) == null
                        ? ValidationResult.Success()
                        : ValidationResult.Error("This name is taken!")));

        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your [green]email[/]:")
                .Validate(x =>
                    userService.GetUserByEmail(x) == null
                        ? ValidationResult.Success()
                        : ValidationResult.Error("This email is taken!")));

        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter [green]password[/]:")
                .PromptStyle("gray")
                .Secret()
                .Validate(pass =>
                    pass.Length is >= 8 and <= 12
                        ? ValidationResult.Success()
                        : ValidationResult.Error("Password must be from 8 to 12 characters long")));

        var hashSalt = PasswordHasher.GenerateSaltedHash(64, password);

        var user = new User()
        {
            Name = name,
            Email = email,
            PasswordHash = hashSalt.Hash,
            PasswordHashSalt = hashSalt.Salt
        };

        var result = userService.AddUser(user);

        if (result)
        {
            AnsiConsole.Write(new Markup($"Created new user with ID [bold yellow]{user.Id}[/]\n"));
        }
        else
        {
            AnsiConsole.Write(new Markup($"Could not create new User\n"));
        }

        WaitForUserInput();
    }
}