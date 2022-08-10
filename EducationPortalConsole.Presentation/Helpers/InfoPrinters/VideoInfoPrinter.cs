using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core.Entities.Materials;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Helpers.InfoPrinters;

public static class VideoInfoPrinter
{
    public static void Print([NotNull] VideoMaterial videoMaterial)
    {
        var table = new Table();

        table.AddColumns("Name", "Value");

        table.AddRow("Id", videoMaterial.Id.ToString());
        table.AddRow("Type", "Article");
        table.AddRow("CreatedBy", UserHelper.GetUsernameById(videoMaterial.CreatedByUserId));
        table.AddRow("Created", videoMaterial.CreatedOn.ToString() ?? String.Empty);
        table.AddRow("UpdatedBy", UserHelper.GetUsernameById(videoMaterial.UpdatedByUserId));
        table.AddRow("Updated", videoMaterial.UpdatedOn.ToString() ?? String.Empty);
        table.AddRow("Duration", videoMaterial.Duration.ToString());
        table.AddRow("Quality", videoMaterial.Quality);

        AnsiConsole.Write(table);
    }
}