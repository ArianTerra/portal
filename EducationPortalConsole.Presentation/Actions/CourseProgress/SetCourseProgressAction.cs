using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.CourseProgress;

public class SetCourseProgressAction : Action
{
    public SetCourseProgressAction()
    {
        Name = "Set Course Progress";
    }

    public override void Run()
    {
        base.Run();

        var courseProgressService = Configuration.Instance.CourseProgressService;
        var user = UserSession.Instance.CurrentUser;

        var courses = courseProgressService.GetSubscribedCourses(user);

        if (!courses.Any())
        {
            AnsiConsole.Write(new Markup($"No [yellow]Courses[/] were found"));
            WaitForUserInput();
            Back();
        }

        var courseSelected = AnsiConsole.Prompt(
            new SelectionPrompt<Course>()
                .MoreChoicesText("[grey](See more...)[/]")
                .AddChoices(courses)
                .UseConverter(x => x.Name)
        );

        var prevProgress = courseProgressService.GetCourseProgress(user, courseSelected);

        var progress = AnsiConsole.Prompt(
            new TextPrompt<int>($"Set course progress (previous was [yellow]{prevProgress}[/]%): ")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]That's not a valid progress percent[/]")
                .Validate(age =>
                {
                    return age switch
                    {
                        <= -1 => ValidationResult.Error("[red]Progress must be larger or equal 0%[/]"),
                        >= 101 => ValidationResult.Error("[red]Progress must be smaller or equal 100%[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));

        courseProgressService.SetCourseProgress(user, courseSelected, progress);

        AnsiConsole.Write(new Markup($"Changed Course [yellow]{courseSelected.Name}[/] progress to [yellow]{progress}[/]%"));

        WaitForUserInput();
        Back();
    }
}