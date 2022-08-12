using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.Presentation.Extensions;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials.EditActions;

public class EditBookAction : Action
{
    private BookMaterial _bookMaterial;

    public EditBookAction(BookMaterial bookMaterial)
    {
        Name = "Edit Book";
        Description = "You can skip field by pressing [blue]<Enter>[/]\n";
        _bookMaterial = bookMaterial;
    }

    public override void Run()
    {
        base.Run();

        IMaterialService materialService = Configuration.Instance.MaterialService;

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]Name[/] (previous: [yellow]{_bookMaterial.Name}[/]):")
                .AllowEmpty());

        if (!name.IsNullOrEmpty())
        {
            _bookMaterial.Name = name;
        }

        var authors = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]Authors[/] using comma (example: Author1,Author2): ")
                .AllowEmpty());

        if (!authors.IsNullOrEmpty())
        {
            _bookMaterial.Authors = authors.Split(",");
        }

        var pages = AnsiConsole.Prompt(
            new TextPrompt<int>($"Enter number of [green]Pages[/] " +
                                   $"(previous: [yellow]{_bookMaterial.Pages}[/]):")
                .AllowEmpty());

        _bookMaterial.Pages = pages;

        var year = AnsiConsole.Prompt(
            new TextPrompt<int>($"Enter [green]Year[/] " +
                                $"(previous: [yellow]{_bookMaterial.Year}[/]):")
                .AllowEmpty());

        _bookMaterial.Year = year;

        var format = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]Format[/] " +
                                $"(previous: [yellow]{_bookMaterial.Format}[/]):")
                .AllowEmpty());

        _bookMaterial.Format = format;

        _bookMaterial.UpdatedOn = DateTime.Now;
        _bookMaterial.UpdatedById = UserSession.Instance.CurrentUser.Id;
        materialService.Update(_bookMaterial);

        AnsiConsole.Write(new Markup($"[green]Material[/] updated\n"));

        WaitForUserInput();
        Back(2);
    }
}