using EducationPortalConsole.BusinessLogic.Helpers.Hasher;
using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Users;

public class UserLoginAction : Action
{
    public UserLoginAction()
    {
        Name = "User Login";
    }
    public override void Run()
    {
        base.Run();
        
        IUserService userService = Configuration.Instance.UserService;
        
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
            AnsiConsole.Write(new Markup($"User with name [bold yellow]{name}[/] does not exist\n"));
            WaitForUserInput();
            
            return;
        }

        if (!PasswordHasher.VerifyPassword(password, user.PasswordHash, user.PasswordHashSalt))
        {
            AnsiConsole.Write(new Markup($"Password is not correct"));
            WaitForUserInput();
            
            return;
        }

        UserSession.Instance.CurrentUser = user;
        
        AnsiConsole.Write(new Markup($"Successfully logged in as [bold yellow]{name}[/]\n"));
        WaitForUserInput();
    }
}