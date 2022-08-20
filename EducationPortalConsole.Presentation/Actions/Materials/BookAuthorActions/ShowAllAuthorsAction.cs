using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Materials.BookAuthorActions;

public class ShowAllAuthorsAction : Action
{
    public ShowAllAuthorsAction()
    {
        Name = "Show All Book Authors";
    }

    public override void Run()
    {
        base.Run();

        var table = new Table();
        table.AddColumns("ID", "Type", "Name", "Created by", "Created", "Updated by", "Updated");

        var service = Configuration.Instance.BookAuthorService;

        foreach (var author in service.GetAll())
        {
            table.AddRow(
                author.Id.ToString(),
                author.GetType().Name,
                author.Name,
                author.CreatedBy?.Name ?? string.Empty,
                author.CreatedOn?.ToString() ?? string.Empty,
                author.UpdatedBy?.Name ?? string.Empty,
                author.UpdatedOn?.ToString() ?? string.Empty
            );
        }

        AnsiConsole.Write(table);

        WaitForUserInput();
        Back();
    }
}