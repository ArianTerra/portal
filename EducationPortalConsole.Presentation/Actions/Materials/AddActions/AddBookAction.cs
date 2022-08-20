using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials.AddActions;

public class AddBookAction : Action
{
    public AddBookAction()
    {
        Name = "Add Book";
    }

    public override void Run()
    {
        base.Run();

        var materialService = Configuration.Instance.BookMaterialService;
        var bookAuthorService = Configuration.Instance.BookAuthorService;

        var name = AnsiConsole.Ask<string>("Enter material [green]Name[/]:");

        var authors = AnsiConsole.Prompt(
            new MultiSelectionPrompt<BookAuthor>()
                .Title("Add selected [green]Book Authors[/] to Book")
                .NotRequired()
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more authors)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle an author, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(bookAuthorService.GetAll())
                .UseConverter(x => x.Name)
        );

        var pages = AnsiConsole.Ask<int>("Enter number of [green]Pages[/]:"); //TODO add validation

        var year = AnsiConsole.Ask<int>("Enter [green]Year[/]:"); //TODO add validation

        var format = AnsiConsole.Ask<string>("Enter [green]Format[/]:"); //TODO add validation

        var material = new BookMaterial()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Pages = pages,
            Year = year,
            Format = format,
            CreatedById = UserSession.Instance.CurrentUser.Id,
            CreatedOn = DateTime.Now
        };

        materialService.Add(material, authors);

        AnsiConsole.Write(new Markup($"Successfully added new Book with ID [bold yellow]{material.Id}[/]\n"));

        WaitForUserInput();
        Back();
    }
}