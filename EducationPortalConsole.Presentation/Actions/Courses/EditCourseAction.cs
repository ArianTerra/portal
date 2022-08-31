using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Courses;

public class EditCourseAction : Action
{
    public EditCourseAction()
    {
        Name = "Edit Course";
        Description = "Select [green]Course[/] you want to edit\n";
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

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]Name[/] (previous: [yellow]{course.Name}[/]):")
                .AllowEmpty());

        if (!string.IsNullOrEmpty(name))
        {
            course.Name = name;
        }

        var materials = AnsiConsole.Prompt(new MultiSelectionPrompt<Material>()
            .Title("Add selected [green]materials[/] to course")
            .NotRequired()
            .MoreChoicesText("[grey](Move up and down to reveal more materials)[/]")
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle a material, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices(materialService.GetAllMaterials())
            .UseConverter(x => x.Name));

        var skills = AnsiConsole.Prompt(new MultiSelectionPrompt<Skill>()
            .Title("Add selected [green]skills[/] to course")
            .NotRequired()
            .MoreChoicesText("[grey](Move up and down to reveal more skills)[/]")
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle a skill, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices(skillService.GetAllSkills())
            .UseConverter(x => x.Name));

        course.UpdatedOn = DateTime.Now;
        course.UpdatedById = UserSession.Instance.CurrentUser.Id;
        courseService.UpdateCourse(course, materials, skills);

        AnsiConsole.Write(new Markup($"[green]Course[/] updated\n"));

        WaitForUserInput();
        Back();
    }
}