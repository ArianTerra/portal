using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.DomainModels.JoinEntities;
using EducationPortal.DataAccess.Repositories;

namespace EducationPortal.BusinessLogic.Services;

public class BookAuthorService
{
    private readonly IGenericRepository<BookAuthor> _repositoryAuthors;
    private readonly IGenericRepository<BookAuthorBookMaterial> _repositoryLinks;

    public BookAuthorService()
    {
        _repositoryAuthors = new GenericRepository<BookAuthor>();
        _repositoryLinks = new GenericRepository<BookAuthorBookMaterial>();
    }

    public BookAuthor? GetBookAuthorById(Guid id)
    {
        return _repositoryAuthors.FindFirst(x => x.Id == id,
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.BookAuthorBookMaterial);
    }

    public IEnumerable<BookAuthor> GetAllBookAuthors()
    {
        return _repositoryAuthors.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.BookAuthorBookMaterial).AsEnumerable();
    }

    public void AddBookAuthor(BookAuthor author)
    {
        _repositoryAuthors.Add(author);
    }

    public void UpdateBookAuthor(BookAuthor author)
    {
        _repositoryAuthors.Update(author);
    }

    public void DeleteBookAuthor(BookAuthor author)
    {
        var linksToDelete = _repositoryLinks.FindAll(x => x.BookMaterialId == author.Id);
        _repositoryLinks.RemoveRange(linksToDelete);

        _repositoryAuthors.Remove(author);
    }
}