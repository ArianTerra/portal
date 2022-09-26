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

public class VideoQualityService : IVideoQualityService
{
    private readonly IGenericRepository<VideoQuality> _repository;

    private readonly IMapper _mapper;

    private readonly IValidator<VideoQuality> _validator;

    public VideoQualityService(IGenericRepository<VideoQuality> repository, IMapper mapper, IValidator<VideoQuality> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<VideoQualityDto>> GetVideoQualityByIdAsync(Guid id)
    {
        var quality = await _repository.FindFirstAsync(x => x.Id == id);

        if (quality == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var qualityDto = _mapper.Map<VideoQualityDto>(quality);

        return Result.Ok(qualityDto);
    }

    public async Task<Result<VideoQualityDto>> GetVideoQualityByNameAsync(string name)
    {
        var quality = await _repository.FindFirstAsync(x => x.Name == name);

        if (quality == null)
        {
            return Result.Fail(new BadRequestError("Name not found"));
        }

        var qualityDto = _mapper.Map<VideoQualityDto>(quality);

        return Result.Ok(qualityDto);
    }

    public async Task<Result<IEnumerable<VideoQualityDto>>> GetVideoQualityPageAsync(int page, int pageSize)
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

        var qualityPage = await _repository.FindAll(
            page: page,
            pageSize: pageSize).ToListAsync();

        var mapped = _mapper.Map<List<VideoQuality>, IEnumerable<VideoQualityDto>>(qualityPage);

        return Result.Ok(mapped);
    }

    public async Task<Result<IEnumerable<VideoQualityDto>>> GetAllVideoQualitiesAsync()
    {
        var qualityPage = await _repository.FindAll().ToListAsync();

        var mapped = _mapper.Map<List<VideoQuality>, IEnumerable<VideoQualityDto>>(qualityPage);

        return Result.Ok(mapped);
    }

    public async Task<Result<int>> GetVideoQualitiesCountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<Result<Guid>> AddVideoQualityAsync(VideoQualityDto dto)
    {
        if (dto == null)
        {
            return Result.Fail(new BadRequestError($"{nameof(VideoQualityDto)} is null"));
        }

        var mapped = _mapper.Map<VideoQuality>(dto);

        if (mapped.Id != Guid.Empty &&
            await _repository.FindFirstAsync(x => x.Id == mapped.Id) != null)
        {
            return Result.Fail(new BadRequestError($"VideoQuality with Id {mapped.Id} already exist in database"));
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

    public async Task<Result> UpdateVideoQualityAsync(VideoQualityDto dto)
    {
        var quality = await _repository.FindFirstAsync(x => x.Id == dto.Id);

        if (quality == null)
        {
            return Result.Fail(new NotFoundError(dto.Id));
        }

        var mapped = _mapper.Map<VideoQuality>(dto);

        var validationResult = await _validator.ValidateAsync(mapped);

        if (quality.Name != mapped.Name &&
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

    public async Task<Result> DeleteVideoQualityByIdAsync(Guid id)
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