using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Courses;

public class DeleteCoursesAction : Action
{
    public DeleteCoursesAction()
    {
        Name = "Delete Courses";
    }

    public override void Run()
    {
        base.Run();

        ICourseService courseService = Configuration.Instance.CourseService;
        
        var courses = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Course>()
                .Title("Delete selected [green]Courses[/]")
                .NotRequired()
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more Courses)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a course, " + 
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(courseService.GetAll())
                .UseConverter(x => x.Name)
        );

        int deleted = 0;
        foreach (var course in courses)
        {
            courseService.Delete(course);
            deleted++;
        }

        AnsiConsole.Write(new Markup($"[yellow]{deleted}[/] [green]Courses[/] were deleted\n"));
        
        WaitForUserInput();
        Back();
    }
}