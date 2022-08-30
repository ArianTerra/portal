using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Courses;

public class AddCourseAction : Action
{
    public AddCourseAction()
    {
        Name = "Add Course";
    }

    public override void Run()
    {
        base.Run();

        var courseService = Configuration.Instance.CourseService;
        var materialService = Configuration.Instance.MaterialService;
        var skillService = Configuration.Instance.SkillService;

        var name = AnsiConsole.Ask<string>("Enter course [green]Name[/]:");

        var materials = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Material>()
                .Title("Add selected [green]materials[/] to course")
                .NotRequired()
                .MoreChoicesText("[grey](Move up and down to reveal more materials)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a material, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(materialService.GetAllMaterials())
                .UseConverter(x => x.Name)
        );

        var skills = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Skill>()
                .Title("Add selected [green]skills[/] to course")
                .NotRequired()
                .MoreChoicesText("[grey](Move up and down to reveal more skills)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a skill, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(skillService.GetAllSkills())
                .UseConverter(x => x.Name)
        );

        var course = new Course()
        {
            Name = name,
            CreatedById = UserSession.Instance.CurrentUser.Id,
            CreatedOn = DateTime.Now
        };

        courseService.AddCourse(course, materials, skills);

        AnsiConsole.Write(new Markup($"Successfully added new Course with ID [bold yellow]{course.Id}[/]\n"));

        WaitForUserInput();
        Back();
    }
}