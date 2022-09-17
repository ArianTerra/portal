using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.CourseProgress;

public class ShowAvailableCoursesAction : Action
{
    public ShowAvailableCoursesAction()
    {
        Name = "Show Available Courses";
    }

    public override void Run()
    {
        base.Run();

        var courseProgressService = Configuration.Instance.CourseProgressService;

        var table = new Table();
        table.AddColumns("Name");

        var user = UserSession.Instance.CurrentUser;
        var courses = courseProgressService.GetAvailableCourses(user).ToList();

        foreach (var course in courses)
        {
            table.AddRow(course.Name);
        }

        AnsiConsole.Write(table);

        WaitForUserInput();
        Back();
    }
}