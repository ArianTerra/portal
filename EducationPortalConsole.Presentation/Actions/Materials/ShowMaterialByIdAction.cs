using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.Presentation.Helpers.InfoPrinters;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials;

public class ShowMaterialByIdAction : Action
{
    public ShowMaterialByIdAction()
    {
        Name = "Show info by ID";
    }
    public override void Run()
    {
        base.Run();

        IMaterialService service = Configuration.Instance.MaterialService;
            
        var id = AnsiConsole.Ask<int>("Enter material [green]ID[/]:");

        Material material = service.GetById(id);

        if (material == null)
        {
            AnsiConsole.Write(new Markup($"Material with [bold green]ID[/] [blue]{id}[/] not found\n"));
        }
        
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