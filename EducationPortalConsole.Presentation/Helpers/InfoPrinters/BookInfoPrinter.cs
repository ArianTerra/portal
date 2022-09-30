using EducationPortalConsole.Core.Entities.Materials;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Helpers.InfoPrinters;

public static class BookInfoPrinter
{
    public static void Print(BookMaterial bookMaterial)
    {
        var table = new Table();

        var authorService = Configuration.Instance.BookAuthorService;
        var authors = bookMaterial.BookAuthorBookMaterial.Where(x => x.BookMaterialId == bookMaterial.Id)
            .Select(x => authorService.GetBookAuthorById(x.BookAuthorId));

        table.AddColumns("Name", "Value");

        table.AddRow("Id", bookMaterial.Id.ToString());
        table.AddRow("Type", "Article");
        table.AddRow("CreatedBy", bookMaterial.CreatedBy?.Name ?? String.Empty);
        table.AddRow("Created", bookMaterial.CreatedOn.ToString() ?? String.Empty);
        table.AddRow("UpdatedBy", bookMaterial.UpdatedBy?.Name ?? String.Empty);
        table.AddRow("Updated", bookMaterial.UpdatedOn.ToString() ?? String.Empty);
        table.AddRow("Authors", string.Join(", ", authors));
        table.AddRow("Pages", bookMaterial.Pages.ToString());
        table.AddRow("Year", bookMaterial.Year.ToString());
        table.AddRow("Format", bookMaterial.Format);

        AnsiConsole.Write(table);
    }
}