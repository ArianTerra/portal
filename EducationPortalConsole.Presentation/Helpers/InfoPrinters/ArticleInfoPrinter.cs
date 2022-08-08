using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core.Entities.Materials;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Helpers.InfoPrinters;

public static class ArticleInfoPrinter
{
    public static void Print([NotNull] ArticleMaterial articleMaterial)
    {
        var table = new Table();

        table.AddColumns("Name", "Value");

        table.AddRow("Id", articleMaterial.Id.ToString());
        table.AddRow("Type", "Article");
        table.AddRow("CreatedBy", UserHelper.GetUsernameById(articleMaterial.CreatedByUserId));
        table.AddRow("Created", articleMaterial.CreatedOn.ToString() ?? String.Empty);
        table.AddRow("UpdatedBy", UserHelper.GetUsernameById(articleMaterial.UpdatedByUserId));
        table.AddRow("Updated", articleMaterial.UpdatedOn.ToString() ?? String.Empty);
        table.AddRow("Date", articleMaterial.Date.ToString());
        table.AddRow("Format", articleMaterial.Source);
        
        AnsiConsole.Write(table);
    }
}