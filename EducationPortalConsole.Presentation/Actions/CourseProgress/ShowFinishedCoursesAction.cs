using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.CourseProgress;

public class ShowFinishedCoursesAction : Action
{
    public ShowFinishedCoursesAction()
    {
        Name = "Show finished Courses";
    }

    public override void Run()
    {
        base.Run();

        var courseProgressService = Configuration.Instance.CourseProgressService;

        var table = new Table();
        table.AddColumns("Name");

        foreach (var course in courseProgressService.GetFinishedCourses(UserSession.Instance.CurrentUser))
        {
            table.AddRow(course.Name);
        }

        AnsiConsole.Write(table);

        WaitForUserInput();
        Back();
    }
}