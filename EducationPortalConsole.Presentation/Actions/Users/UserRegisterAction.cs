using EducationPortalConsole.BusinessLogic.Helpers.Hasher;
using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Users;

public class UserRegisterAction : IAction
{
    public UserRegisterAction(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public void Run()
    {
        IUserService userService = new UserService();
        
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule($"[red]{Name}[/]") {Alignment = Justify.Left});

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your [green]name[/]:")
                .Validate(x => 
                    userService.GetByName(x) == null
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

        int id; // this id generation looks kinda cringe :P
        do
        {
            id = new Random().Next(0, Int32.MaxValue);
        } while (userService.GetById(id) != null);

        var user = new User()
        {
            Id = id,
            Name = name,
            PasswordHash = hashSalt.Hash,
            PasswordHashSalt = hashSalt.Salt
        };

        userService.Add(user);
        
        AnsiConsole.Write(new Markup($"Created new user with ID [bold yellow]{id}[/]"));
    }
}