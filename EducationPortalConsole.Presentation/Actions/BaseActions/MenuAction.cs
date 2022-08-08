using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.BaseActions;

public class MenuAction : Action
{
    public MenuAction()
    {

    }
    
    public List<Action> Actions { get; set; } = new List<Action>();

    public int PageSize { get; set; } = 10;

    public override void Run()
    {
        base.Run();

        var action = AnsiConsole.Prompt(
            new SelectionPrompt<Action>()
                .PageSize(PageSize)
                .MoreChoicesText("[grey](See more...)[/]")
                .AddChoices(Actions)
                .UseConverter(x => x.Name)
        );
        
        action.Run();
    }
}