using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.Presentation.Actions.Materials.EditActions;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials;

public class EditMaterialActions : Action
{
    public EditMaterialActions()
    {
        Name = "Edit Materials";
        Description = "Select material you want to edit:\n";
    }

    public override void Run()
    {
        base.Run();

        IMaterialService materialService = Configuration.Instance.MaterialService;
        
        var material = AnsiConsole.Prompt(
            new SelectionPrompt<Material>()
                .PageSize(10)
                .MoreChoicesText("[grey](See more...)[/]")
                .AddChoices(materialService.GetAll())
                .UseConverter(x => x.Name)
        );

        switch (material)
        {
            case ArticleMaterial articleMaterial:
                new EditArticleAction(articleMaterial).Run();
                break;
            case BookMaterial bookMaterial:
                new EditBookAction(bookMaterial).Run();
                break;
            case VideoMaterial videoMaterial:
                new EditVideoAction(videoMaterial).Run();
                break;
            default:
                throw new ArgumentException("Wrong material type");
        }

        //Back();
    }
}