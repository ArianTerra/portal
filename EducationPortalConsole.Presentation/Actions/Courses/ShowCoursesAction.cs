using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Courses;

public class ShowCoursesAction : Action
{
    public ShowCoursesAction()
    {
        Name = "Show All Courses";
    }

    public override void Run()
    {
        base.Run();

        var courseService = Configuration.Instance.CourseService;

        var table = new Table();

        table.AddColumns("ID", "Name", "Created by", "Created", "Updated by", "Updated");

        foreach (var course in courseService.GetAllCourses().Value)
        {
            table.AddRow(
                course.Id.ToString(),
                course.Name,
                course.CreatedBy?.Name ?? string.Empty,
                course.CreatedOn?.ToString() ?? string.Empty,
                course.UpdatedBy?.Name ?? string.Empty,
                course.UpdatedOn?.ToString() ?? string.Empty
                );
        }

        AnsiConsole.Write(table);

        WaitForUserInput();
        Back();
    }
}