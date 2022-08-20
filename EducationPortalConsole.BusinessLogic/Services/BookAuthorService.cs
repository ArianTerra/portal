using EducationPortalConsole.Core.Entities;
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
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.BookAuthorBookMaterial);
    }

    public IEnumerable<BookAuthor> GetAll()
    {
        return _repositoryAuthors.GetAll(
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.BookAuthorBookMaterial);
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
        foreach (var item in linksToDelete)
        {
            _repositoryLinks.Delete(item);
        }

        return _repositoryAuthors.Delete(author);
    }
}