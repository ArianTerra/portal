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
        table.AddRow("CreatedBy", UserHelper.GetUsernameById(bookMaterial.CreatedByUserId));
        table.AddRow("Created", bookMaterial.CreatedOn.ToString() ?? String.Empty);
        table.AddRow("UpdatedBy", UserHelper.GetUsernameById(bookMaterial.UpdatedByUserId));
        table.AddRow("Updated", bookMaterial.UpdatedOn.ToString() ?? String.Empty);
        table.AddRow("Authors", string.Join(", ", bookMaterial.Authors));
        table.AddRow("Pages", bookMaterial.Pages.ToString());
        table.AddRow("Year", bookMaterial.Year.ToString());
        table.AddRow("Format", bookMaterial.Format);

        AnsiConsole.Write(table);
    }
}