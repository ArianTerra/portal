using EducationPortalConsole.Core.Entities.JoinEntities;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services;

public class BookAuthorService : IBookAuthorService
{
    private readonly IGenericRepository<BookAuthor> _repositoryAuthors;
    private readonly IGenericRepository<BookAuthorBookMaterial> _repositoryLinks;

    public BookAuthorService()
    {
        _repositoryAuthors = new GenericRepository<BookAuthor>();
        _repositoryLinks = new GenericRepository<BookAuthorBookMaterial>();
    }

    public BookAuthor? GetById(Guid id)
    {
        return _repositoryAuthors.FindFirst(x => x.Id == id,
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.BookAuthorBookMaterial);
    }

    public IEnumerable<BookAuthor> GetAll()
    {
        return _repositoryAuthors.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.BookAuthorBookMaterial).AsEnumerable();
    }

    public void Add(BookAuthor author)
    {
        _repositoryAuthors.Add(author);
    }

    public void Update(BookAuthor author)
    {
        _repositoryAuthors.Update(author);
    }

    public bool Delete(BookAuthor author)
    {
        var linksToDelete = _repositoryLinks.FindAll(x => x.BookMaterialId == author.Id);
        _repositoryLinks.RemoveRange(linksToDelete);

        return _repositoryAuthors.Remove(author);
    }
}