using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials.AddActions;

public class AddArticleAction : Action
{
    public AddArticleAction()
    {
        Name = "Add Article";
    }
    public override void Run()
    {
        base.Run();

        IMaterialService materialService = new MaterialService();

        var name = AnsiConsole.Ask<string>("Enter material [green]Name[/]:");

        DateOnly date;
        AnsiConsole.Prompt(
            new TextPrompt<string>("Enter [green]Date[/]:")
                .Validate(x => 
                    DateOnly.TryParse(x, out date)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("Input is not date")));
            
        var source = AnsiConsole.Ask<string>("Enter material [green]Source[/]:");

        var material = new ArticleMaterial()
        {
            Name = name,
            Date = date,
            Source = source,
            CreatedBy = UserSession.Instance.CurrentUser,
            CreatedOn = DateTime.Now
        };
            
        materialService.Add(material);
            
        WaitForUserInput();
        Back();
    }
}