using EducationPortalConsole.BusinessLogic.Services;
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

        ICourseService courseService = Configuration.Instance.CourseService;
        
        var table = new Table();

        table.AddColumns("ID", "Name", "Materials");

        foreach (var course in courseService.GetAll())
        {
            var materialNames = course.Materials.Select(x => x.Name);
            var materials = string.Join(", ", materialNames);
            table.AddRow(course.Id.ToString(), course.Name, materials);
        }
        
        AnsiConsole.Write(table);
        
        WaitForUserInput();
        Back();
    }
}