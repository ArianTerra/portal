using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials.BookAuthorActions;

public class AddBookAuthorAction : Action
{
    public AddBookAuthorAction()
    {
        Name = "Add Book Author";
    }

    public override void Run()
    {
        base.Run();

        var bookAuthorService = Configuration.Instance.BookAuthorService;

        var name = AnsiConsole.Ask<string>("Enter book [green]Name[/]:");

        var author = new BookAuthor()
        {
            Id = Guid.NewGuid(),
            Name = name,
            CreatedOn = DateTime.Now,
            CreatedById = UserSession.Instance.CurrentUser.Id
        };

        bookAuthorService.Add(author);

        AnsiConsole.Write(new Markup($"Successfully added new BookAuthor with ID [bold yellow]{author.Id}[/]\n"));

        WaitForUserInput();
        Back();
    }
}