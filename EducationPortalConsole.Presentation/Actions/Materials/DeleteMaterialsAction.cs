using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials;

public class DeleteMaterialsAction : Action
{
    public DeleteMaterialsAction()
    {
        Name = "Delete Materials";
    }

    public override void Run()
    {
        base.Run();

        var materialService = Configuration.Instance.MaterialService;

        var materials = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Material>()
                .Title("Delete selected [green]materials[/]")
                .NotRequired()
                .MoreChoicesText("[grey](Move up and down to reveal more materials)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a material, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(materialService.GetAllMaterials())
                .UseConverter(x => x.Name)
        );

        foreach (var material in materials)
        {
            materialService.DeleteMaterial(material);
        }

        AnsiConsole.Write(new Markup($"[green]Materials[/] were deleted\n"));

        WaitForUserInput();
        Back();
    }
}