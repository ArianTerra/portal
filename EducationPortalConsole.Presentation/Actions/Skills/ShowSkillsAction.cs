using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Skills;

public class ShowSkillsAction : Action
{
    public ShowSkillsAction()
    {
        Name = "Show All Skills";
    }

    public override void Run()
    {
        base.Run();

        var skillService = Configuration.Instance.SkillService;

        var table = new Table();
        table.AddColumns("ID", "Name", "Created by", "Created", "Updated by", "Updated");

        foreach (var skill in skillService.GetAllSkills().Value)
        {
            table.AddRow(
                skill.Id.ToString(),
                skill.Name,
                skill.CreatedBy?.Name ?? string.Empty,
                skill.CreatedOn?.ToString() ?? string.Empty,
                skill.UpdatedBy?.Name ?? string.Empty,
                skill.UpdatedOn?.ToString() ?? string.Empty
            );
        }

        AnsiConsole.Write(table);

        WaitForUserInput();
        Back();
    }
}