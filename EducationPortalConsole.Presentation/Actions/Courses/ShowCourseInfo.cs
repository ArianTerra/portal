using EducationPortalConsole.Core.Entities;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Courses;

public class ShowCourseInfo : Action
{
    public ShowCourseInfo()
    {
        Name = "Show Course Info";
    }

    public override void Run()
    {
        base.Run();

        var courseService = Configuration.Instance.CourseService;
        var materialService = Configuration.Instance.MaterialService;
        var skillService = Configuration.Instance.SkillService;

        var courseSelected = AnsiConsole.Prompt(
            new SelectionPrompt<Course>()
                .MoreChoicesText("[grey](See more...)[/]")
                .AddChoices(courseService.GetAllCourses())
                .UseConverter(x => x.Name)
        );

        var course = courseService.GetCourseById(courseSelected.Id);

        var materials = course.CourseMaterials.Select(x => materialService.GetMaterialById(x.MaterialId));
        var skills = course.CourseSkills.Select(x => skillService.GetSkillById(x.SkillId).Value);

        var table = new Table();

        table.AddColumns("Name", "Value");
        table.AddRow("Id", course.Id.ToString());
        table.AddRow("Materials", string.Join(", ", materials));
        table.AddRow("Skills", string.Join(", ", skills));
        table.AddRow("CreatedBy", course.CreatedBy?.Name ?? String.Empty);
        table.AddRow("Created", course.CreatedOn.ToString() ?? String.Empty);
        table.AddRow("UpdatedBy", course.UpdatedBy?.Name ?? String.Empty);
        table.AddRow("Updated", course.UpdatedOn.ToString() ?? String.Empty);

        AnsiConsole.Write(table);

        WaitForUserInput();
        Back();
    }
}