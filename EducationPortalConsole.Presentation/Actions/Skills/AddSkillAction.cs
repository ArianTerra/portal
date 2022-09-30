using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Skills;

public class AddSkillAction : Action
{
    public AddSkillAction()
    {
        Name = "Add Skill";
    }

    public override void Run()
    {
        base.Run();

        var skillService = Configuration.Instance.SkillService;

        var name = AnsiConsole.Ask<string>("Enter skill [green]Name[/]:");

        var skill = new Skill()
        {
            Name = name,
            CreatedById = UserSession.Instance.CurrentUser.Id,
            CreatedOn = DateTime.Now
        };

        skillService.AddSkill(skill);

        AnsiConsole.Write(new Markup($"Successfully added new Skill with ID [bold yellow]{skill.Id}[/]\n"));

        WaitForUserInput();
        Back();
    }
}