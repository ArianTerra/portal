using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials.BookAuthorActions;

public class EditBookAuthorAction : Action
{
    public EditBookAuthorAction()
    {
        Name = "Edit Book Author";
    }

    public override void Run()
    {
        base.Run();

        var bookAuthorService = new BookAuthorService();

        var authors = AnsiConsole.Prompt(
            new SelectionPrompt<BookAuthor>()
                .Title("Delete selected [green]materials[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more materials)[/]")
                .AddChoices(bookAuthorService.GetAllBookAuthors())
                .UseConverter(x => x.Name)
        );

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter Author [green]Name[/] (previous: [yellow]{authors.Name}[/]):")
                .AllowEmpty());

        if (!string.IsNullOrEmpty(name))
        {
            authors.Name = name;
        }

        authors.UpdatedOn = DateTime.Now;
        authors.UpdatedById = UserSession.Instance.CurrentUser.Id;

        bookAuthorService.UpdateBookAuthor(authors);

        AnsiConsole.Write(new Markup($"[green]Author[/] updated\n"));

        WaitForUserInput();
        Back();
    }
}