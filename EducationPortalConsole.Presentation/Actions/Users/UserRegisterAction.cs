using EducationPortalConsole.BusinessLogic.Helpers.Hasher;
using EducationPortalConsole.BusinessLogic.Services;
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
            PasswordHash = hashSalt.Hash,
            PasswordHashSalt = hashSalt.Salt
        };

        userService.AddUser(user);

        AnsiConsole.Write(new Markup($"Created new user with ID [bold yellow]{user.Id}[/]\n"));
        WaitForUserInput();
        //ActionProvider.GetAction().Run();
    }
}