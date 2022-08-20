using EducationPortalConsole.BusinessLogic.Services;
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

        ICourseService courseService = Configuration.Instance.CourseService;
        IMaterialService materialService = Configuration.Instance.MaterialService;

        var course = AnsiConsole.Prompt(
            new SelectionPrompt<Course>()
                .MoreChoicesText("[grey](See more...)[/]")
                .AddChoices(courseService.GetAll())
                .UseConverter(x => x.Name)
        );

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]Name[/] (previous: [yellow]{course.Name}[/]):")
                .AllowEmpty());

        if (!string.IsNullOrEmpty(name))
        {
            course.Name = name;
        }

        // TODO fix bug when after loading data from 'DB' course's material isn't marked as selected
        // it seems like .Select() method checks for equality of objects
        var prompt = new MultiSelectionPrompt<Material>()
            .Title("Add selected [green]materials[/] to course")
            .NotRequired()
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more materials)[/]")
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle a material, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices(materialService.GetAll())
            .UseConverter(x => x.Name);

        // foreach (var material in course.Materials) //TODO
        // {
        //     prompt.Select(material);
        // }

        var materials = AnsiConsole.Prompt(prompt);

        course.UpdatedOn = DateTime.Now;
        course.UpdatedById = UserSession.Instance.CurrentUser.Id;
        courseService.Update(course, materials);

        AnsiConsole.Write(new Markup($"[green]Course[/] updated\n"));

        WaitForUserInput();
        Back();
    }
}