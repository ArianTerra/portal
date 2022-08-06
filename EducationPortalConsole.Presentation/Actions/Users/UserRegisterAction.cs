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
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule($"[red]{Name}[/]") {Alignment = Justify.Left});
        var name = AnsiConsole.Ask<string>("What's your [green]name[/]?");
    }
}