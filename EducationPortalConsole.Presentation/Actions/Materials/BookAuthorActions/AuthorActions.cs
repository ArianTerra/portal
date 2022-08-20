using EducationPortalConsole.Presentation.Actions.BaseActions;

namespace EducationPortalConsole.Presentation.Actions.Materials.BookAuthorActions;

public class AuthorActions : MenuAction
{
    public AuthorActions()
    {
        Name = "Book Author Functions";
        Actions = new List<Action>()
        {
            new AddBookAuthorAction(),
            new ShowAllAuthorsAction(),
            new EditBookAuthorAction(),
            new DeleteBooksAuthorAction(),
            new BackAction()
        };
    }
}