using EducationPortalConsole.Core.Entities.Materials;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface IBookAuthorService
{
    BookAuthor? GetById(Guid id);

    IEnumerable<BookAuthor> GetAll();

    void Add(BookAuthor course);

    void Update(BookAuthor course);

    bool Delete(BookAuthor course);
}