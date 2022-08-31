using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.CourseProgress;

public class SubscribeToCourseAction : Action
{
    public SubscribeToCourseAction()
    {
        Name = "Subscribe to Course";
    }

    public override void Run()
    {
        base.Run();

        var courseProgressService = Configuration.Instance.CourseProgressService;
        var user = UserSession.Instance.CurrentUser;

        var courses = courseProgressService.GetAvailableCourses(user);

        if (courses.Any())
        {
            var courseSelected = AnsiConsole.Prompt(
                new SelectionPrompt<Course>()
                    .MoreChoicesText("[grey](See more...)[/]")
                    .AddChoices(courses)
                    .UseConverter(x => x.Name)
            );

            courseProgressService.SubscribeToCourse(user, courseSelected);

            AnsiConsole.Write(new Markup($"Subscribed to Course [yellow]{courseSelected.Name}[/]"));
        }
        else
        {
            AnsiConsole.Write(new Markup($"No [yellow]Courses[/] were found"));
        }

        WaitForUserInput();
        Back();
    }
}