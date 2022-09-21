using System.Linq.Expressions;
using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.DataAccess.DomainModels.AdditionalModels;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.BusinessLogic.Services;

public class BookAuthorService : IBookAuthorService
{
    private readonly IGenericRepository<BookAuthor> _repository;

    private readonly IMapper _mapper;

    private readonly IValidator<BookAuthor> _validator;

    public BookAuthorService(IGenericRepository<BookAuthor> repository, IMapper mapper, IValidator<BookAuthor> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<BookAuthorDto>> GetBookAuthorById(Guid id)
    {
        var author = await _repository.FindFirstAsync(
            filter: x => x.Id == id,
            tracking: true,
            includes: new Expression<Func<BookAuthor, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy
            }
        );

        if (author == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var authorDto = _mapper.Map<BookAuthorDto>(author);

        return Result.Ok(authorDto);
    }

    public async Task<Result<IEnumerable<BookAuthorDto>>> GetBookAuthorsPageAsync(int page, int pageSize)
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

        var authorPage = await _repository.FindAll(
            page: page,
            pageSize: pageSize,
            includes: new Expression<Func<BookAuthor, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy
            }
        ).ToListAsync();

        var mapped = _mapper.Map<List<BookAuthor>, IEnumerable<BookAuthorDto>>(authorPage);

        return Result.Ok(mapped);
    }

    public async Task<Result<IEnumerable<BookAuthorDto>>> GetAllBookAuthorsAsync()
    {
        var authorPage = await _repository.FindAll().ToListAsync();

        var mapped = _mapper.Map<List<BookAuthor>, IEnumerable<BookAuthorDto>>(authorPage);

        return Result.Ok(mapped);
    }

    public async Task<Result<int>> GetBookAuthorCountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<Result<Guid>> AddBookAuthorAsync(BookAuthorDto dto)
    {
        if (dto == null)
        {
            return Result.Fail(new BadRequestError($"{nameof(BookAuthorDto)} is null"));
        }

        var mapped = _mapper.Map<BookAuthor>(dto);

        if (mapped.Id != Guid.Empty &&
            await _repository.FindFirstAsync(x => x.Id == mapped.Id) != null)
        {
            return Result.Fail(new BadRequestError($"BookAuthor with Id {mapped.Id} already exist in database"));
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

    public async Task<Result> UpdateBookAuthorAsync(BookAuthorDto dto)
    {
        var author = await _repository.FindFirstAsync(x => x.Id == dto.Id);

        if (author == null)
        {
            return Result.Fail(new NotFoundError(dto.Id));
        }

        var mapped = _mapper.Map<BookAuthor>(dto);

        var validationResult = await _validator.ValidateAsync(mapped);

        if (author.Name != mapped.Name &&
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

    public async Task<Result> DeleteBookAuthorByIdAsync(Guid id)
    {
        var author = await _repository.FindFirstAsync(x => x.Id == id);

        if (author == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        await _repository.RemoveAsync(author);

        return Result.Ok();
    }
}