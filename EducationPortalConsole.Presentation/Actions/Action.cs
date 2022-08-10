using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions;

public abstract class Action
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public virtual void Run()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule($"[red]{Name}[/]") {Alignment = Justify.Left});

        if (!string.IsNullOrEmpty(Description))
        {
            AnsiConsole.Write(new Markup(Description + "\n\n"));
        }
        
        ActionNavigationProvider.AddToNavigationHistory(this);
    }

    protected static void WaitForUserInput()
    {
        AnsiConsole.Write(new Markup($"\nPress [cyan]any key[/] to continue..."));
        Console.ReadKey();
    }

    protected static void Back(int steps = 1)
    {
        var backToAction = ActionNavigationProvider.GetLastAction(steps);

        backToAction?.Run();
    }

    public string? ToString()
    {
        return Name;
    }
}