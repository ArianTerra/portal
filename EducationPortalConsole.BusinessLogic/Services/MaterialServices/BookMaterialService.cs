using EducationPortalConsole.BusinessLogic.Comparers;
using EducationPortalConsole.Core.Entities.JoinEntities;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services.MaterialServices;

public class BookMaterialService
{
    private readonly IGenericRepository<BookMaterial> _repository;
    private readonly IGenericRepository<BookAuthorBookMaterial> _repositoryLinks;

    public BookMaterialService()
    {
        _repository = new GenericRepository<BookMaterial>();
        _repositoryLinks = new GenericRepository<BookAuthorBookMaterial>();
    }

    public BookMaterial? GetBookById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.BookAuthorBookMaterial);
    }

    public IEnumerable<BookMaterial> GetAllBooks()
    {
        return _repository.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.BookAuthorBookMaterial);
    }

    public void AddBook(BookMaterial material, IEnumerable<BookAuthor> authors)
    {
        _repository.Add(material);

        foreach (var author in authors)
        {
            var link = new BookAuthorBookMaterial()
            {
                BookMaterialId = material.Id,
                BookAuthorId = author.Id
            };

            _repositoryLinks.Add(link);
        }
    }

    public void UpdateBook(BookMaterial material, IEnumerable<BookAuthor> authors)
    {
        var oldLinks = _repositoryLinks.FindAll(x => x.BookMaterialId == material.Id).ToList();
        var newLinks = authors.Select(author => new BookAuthorBookMaterial { BookMaterialId = material.Id, BookAuthorId = author.Id }).ToList();

        var comparer = new BookAuthorBookMaterialComparer();
        var linksToDelete = oldLinks.Except(newLinks, comparer).ToList();
        var linksToAdd = newLinks.Except(oldLinks, comparer).ToList();

        _repositoryLinks.RemoveRange(linksToDelete);
        _repositoryLinks.AddRange(linksToAdd);

        _repository.Update(material);
    }

    public bool DeleteBook(BookMaterial material)
    {
        return _repository.Remove(material);
    }
}