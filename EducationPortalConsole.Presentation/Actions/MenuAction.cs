using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions
{
    public class MenuAction : IAction
    {
        public MenuAction(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public List<IAction> Actions { get; set; } = new List<IAction>();

        public int PageSize { get; set; } = 10;

        public void Run()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule($"[red]{Name}[/]") {Alignment = Justify.Left});
            var action = AnsiConsole.Prompt(
                new SelectionPrompt<IAction>()
                    .PageSize(PageSize)
                    .MoreChoicesText("[grey](See more...)[/]")
                    .AddChoices(Actions)
                    .UseConverter(x => x.Name)
                );
            
            action.Run();
        }
    }
}