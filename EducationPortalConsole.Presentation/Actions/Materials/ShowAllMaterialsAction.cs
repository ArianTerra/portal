using EducationPortalConsole.BusinessLogic.Services;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials;

public class ShowAllMaterialsAction : Action
{
    public ShowAllMaterialsAction()
    {
        Name = "Show All Materials";
    }

    public override void Run()
    {
        base.Run();

        var table = new Table();
        table.AddColumns("ID", "Type", "Name", "Created by", "Created", "Updated by", "Updated");

        IMaterialService service = Configuration.Instance.MaterialService;

        foreach (var material in service.GetAll())
        {
            table.AddRow(
                material.Id.ToString(),
                material.GetType().Name,
                material.Name,
                material.CreatedBy?.Name ?? string.Empty,
                material.CreatedOn?.ToString() ?? string.Empty,
                material.UpdatedBy?.Name ?? string.Empty,
                material.UpdatedOn?.ToString() ?? string.Empty
            );
        }

        AnsiConsole.Write(table);

        WaitForUserInput();
        Back();
    }
}