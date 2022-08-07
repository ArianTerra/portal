using EducationPortalConsole.BusinessLogic.Helpers.Hasher;
using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Users;

public class UserLoginAction : IAction
{
    public UserLoginAction(string name)
    {
        Name = name;
    }
    public string Name { get; }

    public void Run()
    {
        IUserService userService = new UserService();
        
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule($"[red]{Name}[/]") {Alignment = Justify.Left});
        var name = AnsiConsole.Ask<string>("Enter your [green]name[/]:");

        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter [green]password[/]:")
                .PromptStyle("gray")
                .Secret()
                .Validate(pass => 
                    pass.Length is >= 8 and <= 12
                        ? ValidationResult.Success()
                        : ValidationResult.Error("Password must be from 8 to 12 characters long")));

        var user = userService.GetByName(name);
        if (user == null)
        {
            AnsiConsole.Write(new Markup($"User with name [bold yellow]{name}[/] does not exist"));
            return;
        }

        if (!PasswordHasher.VerifyPassword(password, user.PasswordHash, user.PasswordHashSalt))
        {
            AnsiConsole.Write(new Markup($"Password is not correct"));
            return;
        }

        UserSession.Instance.CurrentUser = user;
        
        AnsiConsole.Write(new Markup($"Successfully logged in as [bold yellow]{name}[/]"));
    }
}