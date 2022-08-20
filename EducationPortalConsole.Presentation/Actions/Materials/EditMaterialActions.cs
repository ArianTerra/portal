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

        var articleService = Configuration.Instance.ArticleMaterialService;
        var bookService = Configuration.Instance.BookMaterialService;
        var videoService = Configuration.Instance.VideoMaterialService;

        List<Material> allMaterials = articleService.GetAll()
            .Concat<Material>(bookService.GetAll())
            .Concat(videoService.GetAll()).ToList();

        var material = AnsiConsole.Prompt(
            new SelectionPrompt<Material>()
                .MoreChoicesText("[grey](See more...)[/]")
                .AddChoices(allMaterials)
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