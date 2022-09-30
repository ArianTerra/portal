using EducationPortalConsole.Core.Entities.Materials;
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

        var materialService = Configuration.Instance.BookMaterialService;
        var bookAuthorService = Configuration.Instance.BookAuthorService;

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]Name[/] (previous: [yellow]{_bookMaterial.Name}[/]):")
                .AllowEmpty());

        if (!string.IsNullOrEmpty(name))
        {
            _bookMaterial.Name = name;
        }

        var prompt = new MultiSelectionPrompt<BookAuthor>()
            .Title("Add selected [green]Book Authors[/] to Book")
            .NotRequired()
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more authors)[/]")
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle an author, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices(bookAuthorService.GetAllBookAuthors())
            .UseConverter(x => x.Name);

        var selectedAuthors = _bookMaterial.BookAuthorBookMaterial.Where(x => x.BookMaterialId == _bookMaterial.Id)
            .Select(x => bookAuthorService.GetBookAuthorById(x.BookAuthorId));
        foreach (var author in selectedAuthors)
        {
            prompt = prompt.Select(author);
        }

        var authors = AnsiConsole.Prompt(prompt);

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

        materialService.UpdateBook(_bookMaterial, authors);

        AnsiConsole.Write(new Markup($"[green]Material[/] updated\n"));

        WaitForUserInput();
        Back(2);
    }
}