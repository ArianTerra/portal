using EducationPortalConsole.Core.Entities.Materials;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials.BookAuthorActions;

public class DeleteBooksAuthorAction : Action
{
    public DeleteBooksAuthorAction()
    {
        Name = "Delete Authors";
    }

    public override void Run()
    {
        base.Run();

        var service = Configuration.Instance.BookAuthorService;

        var authors = AnsiConsole.Prompt(
            new MultiSelectionPrompt<BookAuthor>()
                .Title("Delete selected [green]materials[/]")
                .NotRequired()
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more materials)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a material, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(service.GetAll())
                .UseConverter(x => x.Name)
        );

        foreach (var author in authors)
        {
            service.Delete(author);
        }

        WaitForUserInput();
        Back();
    }
}