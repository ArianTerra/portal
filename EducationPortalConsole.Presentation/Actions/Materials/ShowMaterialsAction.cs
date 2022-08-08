using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Presentation.Helpers;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials;

public class ShowMaterialsAction : Action
{
    public ShowMaterialsAction()
    {
        Name = "All Materials";
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
                "TODO", //TODO
                material.Name,
                UserHelper.GetUsernameById(material.CreatedByUserId),
                material.CreatedOn?.ToString() ?? string.Empty,
                UserHelper.GetUsernameById(material.UpdatedByUserId),
                material.UpdatedOn?.ToString() ?? string.Empty
            );
        }
            
        AnsiConsole.Write(table);

        WaitForUserInput();
        Back();
    }
}