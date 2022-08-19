using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core.Entities.Materials;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Helpers.InfoPrinters;

public static class BookInfoPrinter
{
    public static void Print([NotNull] BookMaterial bookMaterial)
    {
        var table = new Table();

        table.AddColumns("Name", "Value");

        table.AddRow("Id", bookMaterial.Id.ToString());
        table.AddRow("Type", "Article");
        table.AddRow("CreatedBy", bookMaterial.CreatedBy?.Name ?? String.Empty);
        table.AddRow("Created", bookMaterial.CreatedOn.ToString() ?? String.Empty);
        table.AddRow("UpdatedBy", bookMaterial.UpdatedBy?.Name ?? String.Empty);
        table.AddRow("Updated", bookMaterial.UpdatedOn.ToString() ?? String.Empty);
        //table.AddRow("Authors", string.Join(", ", bookMaterial.Authors));
        table.AddRow("Pages", bookMaterial.Pages.ToString());
        table.AddRow("Year", bookMaterial.Year.ToString());
        table.AddRow("Format", bookMaterial.Format);

        AnsiConsole.Write(table);
    }
}