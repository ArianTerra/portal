using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Courses;

public class AddCourseAction : Action
{
    public AddCourseAction()
    {
        Name = "Add Course";
    }

    public override void Run()
    {
        base.Run();

        ICourseService courseService = Configuration.Instance.CourseService;
        IMaterialService materialService = Configuration.Instance.MaterialService;

        var name = AnsiConsole.Ask<string>("Enter course [green]Name[/]:");

        var materials = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Material>()
                .Title("Add selected [green]materials[/] to course")
                .NotRequired()
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more materials)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a material, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(materialService.GetAll())
                .UseConverter(x => x.Name)
        );

        var course = new Course()
        {
            Name = name,
            CreatedById = UserSession.Instance.CurrentUser.Id,
            CreatedOn = DateTime.Now
        };

        courseService.Add(course, materials);

        AnsiConsole.Write(new Markup($"Successfully added new Course with ID [bold yellow]{course.Id}[/]\n"));

        WaitForUserInput();
        Back();
    }
}