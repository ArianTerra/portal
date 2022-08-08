using EducationPortalConsole.BusinessLogic.Services;
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
        
        IMaterialService materialService = Configuration.Instance.MaterialService;

        var name = AnsiConsole.Ask<string>("Enter material [green]Name[/]:");

        var authors = AnsiConsole.Ask<string>("Enter [green]Authors[/] using comma (example: Author1,Author2):");
        
        var pages = AnsiConsole.Ask<int>("Enter number of [green]Pages[/]:"); //TODO add validation
        
        var year = AnsiConsole.Ask<int>("Enter [green]Year[/]:"); //TODO add validation
        
        var format = AnsiConsole.Ask<string>("Enter [green]Format[/]:"); //TODO add validation
        
        var material = new BookMaterial()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Authors = authors.Split(','),
            Pages = pages,
            Year = year,
            Format = format,
            CreatedByUserId = UserSession.Instance.CurrentUser.Id,
            CreatedOn = DateTime.Now
        };

        materialService.Add(material);
        
        AnsiConsole.Write(new Markup($"Successfully added new Book with ID [bold yellow]{material.Id}[/]\n"));
        
        WaitForUserInput();
        Back();
    }
}