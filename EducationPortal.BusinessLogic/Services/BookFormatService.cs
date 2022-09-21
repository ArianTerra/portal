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

public class BookFormatService : IBookFormatService
{
    private readonly IGenericRepository<BookFormat> _repository;

    private readonly IMapper _mapper;

    private readonly IValidator<BookFormatDto> _validator;

    public BookFormatService(IGenericRepository<BookFormat> repository, IMapper mapper, IValidator<BookFormatDto> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<BookFormatDto>> GetBookFormatByIdAsync(Guid id)
    {
        var format = await _repository.FindFirstAsync(x => x.Id == id);

        if (format == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var bookFormatDto = _mapper.Map<BookFormatDto>(format);

        return Result.Ok(bookFormatDto);
    }

    public async Task<Result<BookFormatDto>> GetBookFormatByNameAsync(string name)
    {
        var bookFormat = await _repository.FindFirstAsync(x => x.Name == name);

        if (bookFormat == null)
        {
            return Result.Fail(new BadRequestError("Name not found"));
        }

        var bookFormatDto = _mapper.Map<BookFormatDto>(bookFormat);

        return Result.Ok(bookFormatDto);
    }

    public async Task<Result<IEnumerable<BookFormatDto>>> GetBookFormatPageAsync(int page, int pageSize)
    {
        int itemsCount = await _repository.CountAsync();
        int pagesCount = (int)Math.Ceiling((double)itemsCount / pageSize);

        if (pagesCount == 0) //TODO is this right?
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

        var qualityPage = await _repository.FindAll(
            page: page,
            pageSize: pageSize).ToListAsync();

        var mapped = _mapper.Map<List<BookFormat>, IEnumerable<BookFormatDto>>(qualityPage);

        return Result.Ok(mapped);
    }

    public async Task<Result<IEnumerable<BookFormatDto>>> GetAllBookFormatesAsync()
    {
        var qualityPage = await _repository.FindAll().ToListAsync();

        var mapped = _mapper.Map<List<BookFormat>, IEnumerable<BookFormatDto>>(qualityPage);

        return Result.Ok(mapped);
    }

    public async Task<Result<int>> GetBookFormatsCountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<Result<Guid>> AddBookFormatAsync(BookFormatDto dto)
    {
        if (dto == null)
        {
            return Result.Fail(new BadRequestError($"{nameof(BookFormatDto)} is null"));
        }

        var validationResult = await _validator.ValidateAsync(dto);

        if (await _repository.FindFirstAsync(x => x.Name == dto.Name) != null)
        {
            validationResult.Errors.Add(
                new ValidationFailure(nameof(dto.Name), "Name must be unique"));
        }

        var mapped = _mapper.Map<BookFormat>(dto);

        if (mapped.Id != Guid.Empty &&
            await _repository.FindFirstAsync(x => x.Id == mapped.Id) != null)
        {
            return Result.Fail(new BadRequestError($"BookFormat with Id {mapped.Id} already exist in database"));
        }

        if (!validationResult.IsValid)
        {
            return Result.Fail(new ValidationError(validationResult));
        }

        await _repository.AddAsync(mapped);

        return Result.Ok(mapped.Id);
    }

    public async Task<Result> UpdateBookFormatAsync(BookFormatDto dto)
    {
        var quality = await _repository.FindFirstAsync(x => x.Id == dto.Id);

        if (quality == null)
        {
            return Result.Fail(new NotFoundError(dto.Id));
        }

        var validationResult = await _validator.ValidateAsync(dto);

        if (quality.Name != dto.Name &&
            await _repository.FindFirstAsync(x => x.Name == dto.Name) != null)
        {
            validationResult.Errors.Add(
                new ValidationFailure(nameof(dto.Name), "Name must be unique"));
        }

        var mapped = _mapper.Map<BookFormat>(dto);

        if (!validationResult.IsValid)
        {
            return Result.Fail(new ValidationError(validationResult));
        }

        await _repository.UpdateAsync(mapped);

        return Result.Ok();
    }

    public async Task<Result> DeleteBookFormatByIdAsync(Guid id)
    {
        var quality = await _repository.FindFirstAsync(x => x.Id == id);

        if (quality == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        await _repository.RemoveAsync(quality);

        return Result.Ok();
    }
}