using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials.AddActions;

public class AddVideoAction : Action
{
    public AddVideoAction()
    {
        Name = "Add Video";
    }

    public override void Run()
    {
        base.Run();

        IMaterialService materialService = Configuration.Instance.MaterialService;

        var name = AnsiConsole.Ask<string>("Enter material [green]Name[/]:");

        TimeSpan time = new TimeSpan();
        AnsiConsole.Prompt(
            new TextPrompt<string>("Enter [green]Duration[/] (for example, 10:50):")
                .Validate(x =>
                    TimeSpan.TryParse(x, out time)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("Input is not correct")));

        var quality = AnsiConsole.Ask<string>("Enter [green]Quality[/]:"); //TODO change quality to enum maybe

        var material = new VideoMaterial()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Duration = time,
            Quality = quality,
            CreatedById = UserSession.Instance.CurrentUser.Id,
            CreatedOn = DateTime.Now
        };

        materialService.Add(material);

        AnsiConsole.Write(new Markup($"Successfully added new Video with ID [bold yellow]{material.Id}[/]\n"));

        WaitForUserInput();
        Back();
    }
}