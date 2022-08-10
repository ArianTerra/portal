using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.Presentation.Helpers.InfoPrinters;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials;

public class ShowMaterialInfo : Action
{
    public ShowMaterialInfo()
    {
        Name = "Show material info";
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
        
        if (material is ArticleMaterial articleMaterial) //TODO change this to smth appropriate
        {
            ArticleInfoPrinter.Print(articleMaterial);
        }
            
        if (material is BookMaterial bookMaterial)
        {
            BookInfoPrinter.Print(bookMaterial);
        }
        
        if (material is VideoMaterial videoMaterial)
        {
            VideoInfoPrinter.Print(videoMaterial);
        }
            
        WaitForUserInput();
        Back();
    }
}