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

        var articleService = Configuration.Instance.ArticleMaterialService;
        var bookService = Configuration.Instance.BookMaterialService;
        var videoService = Configuration.Instance.VideoMaterialService;

        List<Material> allMaterials = articleService.GetAll()
            .Concat<Material>(bookService.GetAll())
            .Concat(videoService.GetAll()).ToList();

        if (allMaterials.Any())
        {
            var material = AnsiConsole.Prompt(
                new SelectionPrompt<Material>()
                    .MoreChoicesText("[grey](See more...)[/]")
                    .AddChoices(allMaterials)
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
        }
        else
        {
            AnsiConsole.Write("No materials found\n");
        }

        WaitForUserInput();
        Back();
    }
}