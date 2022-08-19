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

        IMaterialService materialService = Configuration.Instance.MaterialService;
        ICourseService courseService = Configuration.Instance.CourseService;

        var materials = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Material>()
                .Title("Delete selected [green]materials[/]")
                .NotRequired()
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more materials)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a material, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(materialService.GetAll())
                .UseConverter(x => x.Name)
        );

        int deleted = 0;
        foreach (var material in materials)
        {
            var skip = false;
            foreach (var course in courseService.GetAll())
            {
                // if (course.Materials.Contains(material)) //TODO
                // {
                //     AnsiConsole.Write(
                //         new Markup($"Cannot delete material with [green]ID[/] [yellow]{material.Id}[/] because it used in " +
                //                    $"course with [green]ID[/] [yellow]{course.Id}[/]\n"));
                //     skip = true;
                // }
            }

            if (skip)
            {
                continue;
            }

            materialService.Delete(material);
            deleted++;
        }

        AnsiConsole.Write(new Markup($"[yellow]{deleted}[/] [green]Materials[/] were deleted\n"));

        WaitForUserInput();
        Back();
    }
}