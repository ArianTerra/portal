using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.JoinEntities;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services.MaterialServices;

public class BookMaterialService
{
    private readonly IGenericRepository<BookMaterial> _repository;
    private readonly IGenericRepository<BookAuthor> _repositoryAuthors;
    private readonly IGenericRepository<BookMaterialBookAuthor> _repositoryBookAuthors;

    public BookMaterialService()
    {
        _repository = new GenericRepository<BookMaterial>();
        _repositoryAuthors = new GenericRepository<BookAuthor>();
        _repositoryBookAuthors = new GenericRepository<BookMaterialBookAuthor>();
    }

    public BookMaterial? GetById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.BookMaterialBookAuthors);
    }

    public IEnumerable<BookMaterial> GetAll()
    {
        return _repository.GetAll(
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.BookMaterialBookAuthors);
    }

    public void Add(BookMaterial material, IEnumerable<BookAuthor> authors)
    {
        _repository.Add(material);

        foreach (var author in authors)
        {
            _repositoryAuthors.Add(author);

            var link = new BookMaterialBookAuthor()
            {
                BookMaterialId = material.Id,
                BookAuthorId = author.Id
            };

            _repositoryBookAuthors.Add(link);
        }
    }

    public void Update(BookMaterial material, IEnumerable<BookAuthor> authors)
    {
        var linksToDelete = _repositoryBookAuthors.FindAll(x => x.BookMaterialId == material.Id);
        foreach (var item in linksToDelete)
        {
            _repositoryBookAuthors.Delete(item);

            var author = _repositoryAuthors.FindFirst(x => x.Id == item.BookAuthorId);
            if (author != null)
            {
                _repositoryAuthors.Delete(author);
            }
        }

        foreach (var author in authors)
        {
            _repositoryAuthors.Add(author);

            var link = new BookMaterialBookAuthor()
            {
                BookMaterialId = material.Id,
                BookAuthorId = author.Id
            };

            _repositoryBookAuthors.Add(link);
        }

        _repository.Update(material);
    }

    public bool Delete(BookMaterial material) //todo test it
    {
        return _repository.Delete(material);
    }
}