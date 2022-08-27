using EducationPortalConsole.Core.Entities.Materials;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface IBookAuthorService
{
    BookAuthor? GetBookAuthorById(Guid id);

    IEnumerable<BookAuthor> GetAllBookAuthors();

    void AddBookAuthor(BookAuthor course);

    void UpdateBookAuthor(BookAuthor course);

    bool DeleteBookAuthor(BookAuthor course);
}