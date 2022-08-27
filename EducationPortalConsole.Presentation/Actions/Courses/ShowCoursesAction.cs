using EducationPortalConsole.Core.Entities;
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
        var materialService = Configuration.Instance.MaterialService;

        var table = new Table();

        table.AddColumns("ID", "Name", "Materials", "Created by", "Created", "Updated by", "Updated");

        foreach (var course in courseService.GetAllCourses())
        {
            var materialIds = course.CourseMaterials.Where(x => x.CourseId == course.Id)
                .Select(x => x.MaterialId);
            var materials = new List<Material>();
            foreach (var id in materialIds)
            {
                materials.Add(materialService.GetMaterialById(id));
            }

            // var matNames =

            table.AddRow(
                course.Id.ToString(),
                course.Name,
                string.Join(", ", materials.Select(x => x.Name)),
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