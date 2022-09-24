using System.Linq.Expressions;
using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.BusinessLogic.Utils.Comparers;
using EducationPortal.DataAccess.DomainModels.AdditionalModels;
using EducationPortal.DataAccess.DomainModels.JoinEntities;
using EducationPortal.DataAccess.DomainModels.Materials;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.BusinessLogic.Services;

public class BookMaterialService : IBookMaterialService
{
    private readonly IGenericRepository<BookMaterial> _repository;

    private readonly IGenericRepository<BookAuthor> _repositoryAuthors;

    private readonly IGenericRepository<BookAuthorBookMaterial> _repositoryLinks;

    private readonly IMapper _mapper;

    private readonly IValidator<BookMaterial> _validator;

    public BookMaterialService(IGenericRepository<BookMaterial> repository,
        IGenericRepository<BookAuthorBookMaterial> repositoryLinks,
        IMapper mapper,
        IValidator<BookMaterial> validator,
        IGenericRepository<BookAuthor> repositoryAuthors)
    {
        _repository = repository;
        _repositoryLinks = repositoryLinks;
        _mapper = mapper;
        _validator = validator;
        _repositoryAuthors = repositoryAuthors;
    }

    public async Task<Result<BookMaterialDto>> GetBookByIdAsync(Guid id)
    {
        var video = await _repository.FindFirstAsync(
            filter: x => x.Id == id,
            includes: new Expression<Func<BookMaterial, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy,
                x => x.BookFormat
            }
        );

        if (video == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var bookDto = _mapper.Map<BookMaterialDto>(video);
        bookDto.Authors = await GetAuthorsAsync(id);

        return Result.Ok(bookDto);
    }

    public async Task<Result<IEnumerable<BookMaterialDto>>> GetBooksPageAsync(int page, int pageSize)
    {
        int itemsCount = await _repository.CountAsync();
        int pagesCount = (int)Math.Ceiling((double)itemsCount / pageSize);

        if (pagesCount == 0)
        {
            pagesCount = 1;
        }

        if (pageSize <= 0)
        {
            return Result.Fail(new BadRequestError("Page size should be bigger than 0"));
        }

        if (page <= 0 || page > pagesCount)
        {
            return Result.Fail(new BadRequestError("Page does not exist"));
        }

        var videoPage = await _repository.FindAll(
            page: page,
            pageSize: pageSize,
            includes: new Expression<Func<BookMaterial, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy,
                x => x.BookFormat
            }
        ).ToListAsync();

        var mapped = _mapper.Map<List<BookMaterial>, IEnumerable<BookMaterialDto>>(videoPage);

        return Result.Ok(mapped);
    }

    public async Task<Result<int>> GetBooksCountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<Result<Guid>> AddBookAsync(BookMaterialDto dto, Guid createdById)
    {
        if (dto == null)
        {
            return Result.Fail(new BadRequestError($"{nameof(BookMaterialDto)} is null"));
        }

        var mapped = _mapper.Map<BookMaterial>(dto);
        mapped.CreatedById = createdById;
        mapped.CreatedOn = DateTime.Now;

        if (mapped.Id != Guid.Empty &&
            await _repository.FindFirstAsync(x => x.Id == mapped.Id) != null)
        {
            return Result.Fail(new BadRequestError($"Book with Id {mapped.Id} already exist in database"));
        }

        var validationResult = await _validator.ValidateAsync(mapped);

        if (await _repository.FindFirstAsync(x => x.Name == mapped.Name) != null)
        {
            validationResult.Errors.Add(
                new ValidationFailure(nameof(mapped.Name), "Name must be unique"));
        }

        if (!validationResult.IsValid)
        {
            return Result.Fail(new ValidationError(validationResult));
        }

        await _repository.AddAsync(mapped);

        return Result.Ok(mapped.Id);
    }

    public async Task<Result> UpdateBookAsync(BookMaterialDto dto, Guid updatedById)
    {
        var book = await _repository.FindFirstAsync(x => x.Id == dto.Id);

        if (book == null)
        {
            return Result.Fail(new NotFoundError(dto.Id));
        }

        var mapped = _mapper.Map<BookMaterial>(dto);
        mapped.CreatedById = book.CreatedById;
        mapped.CreatedOn = book.CreatedOn;
        mapped.UpdatedById = updatedById;
        mapped.UpdatedOn = DateTime.Now;

        var validationResult = await _validator.ValidateAsync(mapped);

        if (book.Name != mapped.Name &&
            await _repository.FindFirstAsync(x => x.Name == mapped.Name) != null)
        {
            validationResult.Errors.Add(
                new ValidationFailure(nameof(mapped.Name), "Name must be unique"));
        }

        if (!validationResult.IsValid)
        {
            return Result.Fail(new ValidationError(validationResult));
        }

        await _repository.UpdateAsync(mapped);

        return Result.Ok();
    }

    public async Task<Result> DeleteBookByIdAsync(Guid id)
    {
        var book = await _repository.FindFirstAsync(x => x.Id == id);

        if (book == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        await _repository.RemoveAsync(book);

        return Result.Ok();
    }

    public async Task<Result> AddAuthorsToBookAsync(Guid id, IEnumerable<BookAuthorDto> authors)
    {
        var book = await _repository.FindFirstAsync(
            filter: x => x.Id == id,
            includes: new Expression<Func<BookMaterial, object>>[]
            {
                x => x.BookAuthorBookMaterial
            });

        if (book == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var oldLinks = book.BookAuthorBookMaterial.Where(x => x.BookMaterialId == book.Id);
        var newLinks = authors.Select(author => new BookAuthorBookMaterial { BookMaterialId = book.Id, BookAuthorId = author.Id }).ToList();

        var comparer = new BookAuthorBookMaterialComparer();
        var linksToDelete = oldLinks.Except(newLinks, comparer).ToList();
        var linksToAdd = newLinks.Except(oldLinks, comparer).ToList();

        await _repositoryLinks.RemoveRangeAsync(linksToDelete);
        await _repositoryLinks.AddRangeAsync(linksToAdd);

        return Result.Ok();
    }

    public async Task<IEnumerable<BookAuthorDto>> GetAuthorsAsync(Guid id)
    {
        var authorsIds = await _repositoryLinks.FindAll(x => x.BookMaterialId == id).Select(x => x.BookAuthorId)
            .ToListAsync();

        var authors = new List<BookAuthor>();
        foreach (var authorId in authorsIds)
        {
            var author = await _repositoryAuthors.FindFirstAsync(x => x.Id == authorId);
            authors.Add(author);
        }

        var mapped = _mapper.Map<IEnumerable<BookAuthor>, IEnumerable<BookAuthorDto>>(authors);

        return mapped;
    }
}