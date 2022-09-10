using EducationPortalConsole.Core.Entities;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Skills;

public class DeleteSkillsAction : Action
{
    public DeleteSkillsAction()
    {
        Name = "Delete Skills";
    }

    public override void Run()
    {
        base.Run();

        var skillService = Configuration.Instance.SkillService;

        var skills = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Skill>()
                .Title("Delete selected [green]Skills[/]")
                .NotRequired()
                .MoreChoicesText("[grey](Move up and down to reveal more Skills)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a skill, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(skillService.GetAllSkills().Value)
                .UseConverter(x => x.Name)
        );

        skillService.DeleteSkills(skills);

        AnsiConsole.Write(new Markup($"Selected [green]Skills[/] were deleted\n"));

        WaitForUserInput();
        Back();
    }
}