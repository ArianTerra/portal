using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Skills;

public class EditSkillAction : Action
{
    public EditSkillAction()
    {
        Name = "Edit Skill";
    }

    public override void Run()
    {
        base.Run();

        var skillService = Configuration.Instance.SkillService;

        var skill = AnsiConsole.Prompt(
            new SelectionPrompt<Skill>()
                .MoreChoicesText("[grey](See more...)[/]")
                .AddChoices(skillService.GetAllSkills())
                .UseConverter(x => x.Name)
        );

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]Name[/] (previous: [yellow]{skill.Name}[/]):")
                .AllowEmpty());

        skill.Name = name;
        skill.UpdatedById = UserSession.Instance.CurrentUser.Id;
        skill.UpdatedOn = DateTime.Now;

        skillService.UpdateSkill(skill);

        AnsiConsole.Write(new Markup($"[green]Skill[/] updated\n"));

        WaitForUserInput();
        Back();
    }
}