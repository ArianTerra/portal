using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.Presentation.Extensions;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials.EditActions;

public class EditArticleAction : Action
{
    private ArticleMaterial _articleMaterial;

    public EditArticleAction(ArticleMaterial articleMaterial)
    {
        Name = "Edit Article";
        Description = "You can skip field by pressing [blue]<Enter>[/]\n";
        _articleMaterial = articleMaterial;
    }

    public override void Run()
    {
        base.Run();
        IMaterialService materialService = Configuration.Instance.MaterialService;

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]Name[/] (previous: [yellow]{_articleMaterial.Name}[/]):")
                .AllowEmpty());

        if (!name.IsNullOrEmpty())
        {
            _articleMaterial.Name = name;
        }

        //Date edit
        DateTime date = new DateTime();
        var dateStr = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]Date[/] (previous: [yellow]{_articleMaterial.Date.Date}[/]):")
                .AllowEmpty()
        );

        if (!dateStr.IsNullOrEmpty())
        {
            if (DateTime.TryParse(dateStr, out date))
            {
                _articleMaterial.Date = date;
            }
            else
            {
                AnsiConsole.Write(new Markup("[red]Input Date[/] was in wrong format, it was not changed\n"));
            }
        }

        var source = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]Source[/] (previous: [yellow]{_articleMaterial.Source}[/]):")
                .AllowEmpty());

        if (!source.IsNullOrEmpty())
        {
            _articleMaterial.Source = source;
        }

        _articleMaterial.UpdatedOn = DateTime.Now;
        _articleMaterial.UpdatedById = UserSession.Instance.CurrentUser.Id;

        materialService.Update(_articleMaterial);

        AnsiConsole.Write(new Markup($"[green]Material[/] updated\n"));

        WaitForUserInput();
        Back(2);
    }
}