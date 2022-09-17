using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.CourseProgress;

public class ShowCoursesInProgressAction : Action
{
    public ShowCoursesInProgressAction()
    {
        Name = "Show Courses in Progress";
    }

    public override void Run()
    {
        base.Run();

        var courseProgressService = Configuration.Instance.CourseProgressService;
        var user = UserSession.Instance.CurrentUser;
        var table = new Table();
        table.AddColumns("Name", "Progress");

        foreach (var course in courseProgressService.GetInProgressCourses(user))
        {
            var progress = courseProgressService.GetCourseProgress(user, course);
            table.AddRow(course.Name, $"{progress}%");
        }

        AnsiConsole.Write(table);

        WaitForUserInput();
        Back();
    }
}