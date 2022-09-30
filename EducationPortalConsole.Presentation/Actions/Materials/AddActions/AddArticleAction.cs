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

        var materialService = Configuration.Instance.ArticleMaterialService;

        var name = AnsiConsole.Ask<string>("Enter material [green]Name[/]:");

        DateTime date = new DateTime();
        AnsiConsole.Prompt(
            new TextPrompt<string>("Enter [green]Date[/]:")
                .Validate(x =>
                    DateTime.TryParse(x, out date)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("Input is not date")));

        var source = AnsiConsole.Ask<string>("Enter material [green]Source[/]:");

        var material = new ArticleMaterial()
        {
            Name = name,
            Date = date,
            Source = source,
            CreatedById = UserSession.Instance.CurrentUser.Id,
            CreatedOn = DateTime.Now
        };

        var result = materialService.AddArticle(material);

        if (result.IsSuccess)
        {
            AnsiConsole.Write(new Markup($"Successfully added new Article with ID [bold yellow]{material.Id}[/]\n"));
        }
        else
        {
            foreach (var error in result.Errors)
            {
                AnsiConsole.Write(error.Message + "\n");
            }
        }

        WaitForUserInput();
        Back();
    }
}