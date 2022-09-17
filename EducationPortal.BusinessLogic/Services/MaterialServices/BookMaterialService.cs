using EducationPortal.BusinessLogic.Resources.ErrorMessages;
using EducationPortal.BusinessLogic.Utils.Comparers;
using EducationPortal.BusinessLogic.Validators.FluentValidation;
using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.DomainModels.JoinEntities;
using EducationPortal.DataAccess.DomainModels.Materials;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using FluentValidation;

namespace EducationPortal.BusinessLogic.Services.MaterialServices;

public class BookMaterialService
{
    private readonly IGenericRepository<BookMaterial> _repository;
    private readonly IGenericRepository<BookAuthorBookMaterial> _repositoryLinks;

    public BookMaterialService()
    {
        _repository = new GenericRepository<BookMaterial>();
        _repositoryLinks = new GenericRepository<BookAuthorBookMaterial>();
    }

    public BookMaterialService(GenericRepository<BookMaterial> repository, GenericRepository<BookAuthorBookMaterial> links)
    {
        _repository = repository;
        _repositoryLinks = links;
    }

    public Result<BookMaterial> GetBookById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(ErrorMessages.GuidEmpty);
        }

        var result = Result.Try(() =>
            _repository.FindFirst(
                x => x.Id == id,
                true,
                x => x.CreatedBy,
                x => x.UpdatedBy,
                x => x.BookAuthorBookMaterial));

        if (result.IsSuccess && result.Value == null)
        {
            return Result.Fail(ErrorMessages.NotFound);
        }

        return result;
    }

    public Result<List<BookMaterial>> GetAllBooks()
    {
        var result = Result.Try(() =>
            _repository.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.BookAuthorBookMaterial).ToList());

        return result;
    }

    public Result AddBook(BookMaterial material, IEnumerable<BookAuthor> authors)
    {
        var validationResult = ValidateBook(material);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }

        var result = Result.Try(() =>
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
        });

        return result;
    }

    public Result UpdateBook(BookMaterial material, IEnumerable<BookAuthor> authors)
    {
        var validationResult = ValidateBook(material);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }

        var result = Result.Try(() =>
        {
            var oldLinks = _repositoryLinks.FindAll(x => x.BookMaterialId == material.Id).ToList();
            var newLinks = authors.Select(author => new BookAuthorBookMaterial { BookMaterialId = material.Id, BookAuthorId = author.Id }).ToList();

            var comparer = new BookAuthorBookMaterialComparer();
            var linksToDelete = oldLinks.Except(newLinks, comparer).ToList();
            var linksToAdd = newLinks.Except(oldLinks, comparer).ToList();

            _repositoryLinks.RemoveRange(linksToDelete);
            _repositoryLinks.AddRange(linksToAdd);

            _repository.Update(material);
        });

        return result;
    }

    public Result DeleteBook(BookMaterial material)
    {
        if (material == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull);
        }

        return Result.Try(() => _repository.Remove(material));
    }

    private Result ValidateBook(BookMaterial material)
    {
        if (material == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull);
        }

        var validator = new BookMaterialValidator();

        try
        {
            validator.ValidateAndThrow(material);
        }
        catch (ValidationException ex)
        {
            return Result.Fail(new Error(ErrorMessages.ValidationError).CausedBy(ex));
        }

        return Result.Ok();
    }
}