using System.Linq.Expressions;
using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.DataAccess.DomainModels.Materials;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.BusinessLogic.Services;

public class VideoMaterialService : IVideoMaterialService
{
    private readonly IGenericRepository<VideoMaterial> _repository;

    private readonly IMapper _mapper;

    private readonly IValidator<VideoMaterial> _validator;

    public VideoMaterialService(IGenericRepository<VideoMaterial> repository, IMapper mapper, IValidator<VideoMaterial> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<VideoMaterialDto>> GetVideoByIdAsync(Guid id)
    {
        var video = await _repository.FindFirstAsync(
            filter: x => x.Id == id,
            tracking: true,
            includes: new Expression<Func<VideoMaterial, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy,
                x => x.Quality
            }
        );

        if (video == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var videoDto = _mapper.Map<VideoMaterialDto>(video);

        return Result.Ok(videoDto);
    }

    public async Task<Result<IEnumerable<VideoMaterialDto>>> GetVideosPageAsync(int page, int pageSize)
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
            includes: new Expression<Func<VideoMaterial, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy,
                x => x.Quality
            }
        ).ToListAsync();

        var mapped = _mapper.Map<List<VideoMaterial>, IEnumerable<VideoMaterialDto>>(videoPage);

        return Result.Ok(mapped);
    }

    public async Task<Result<int>> GetVideosCountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<Result<Guid>> AddVideoAsync(VideoMaterialDto dto, Guid createdById)
    {
        if (dto == null)
        {
            return Result.Fail(new BadRequestError($"{nameof(VideoMaterialDto)} is null"));
        }

        var mapped = _mapper.Map<VideoMaterial>(dto);
        mapped.CreatedById = createdById;
        mapped.CreatedOn = DateTime.Now;

        if (mapped.Id != Guid.Empty &&
            await _repository.FindFirstAsync(x => x.Id == mapped.Id) != null)
        {
            return Result.Fail(new BadRequestError($"Video with Id {mapped.Id} already exist in database"));
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

    public async Task<Result> UpdateVideoAsync(VideoMaterialDto dto, Guid updatedById)
    {
        var video = await _repository.FindFirstAsync(x => x.Id == dto.Id);

        if (video == null)
        {
            return Result.Fail(new NotFoundError(dto.Id));
        }

        var mapped = _mapper.Map<VideoMaterial>(dto);
        mapped.CreatedById = video.CreatedById;
        mapped.CreatedOn = video.CreatedOn;
        mapped.UpdatedById = updatedById;
        mapped.UpdatedOn = DateTime.Now;

        var validationResult = await _validator.ValidateAsync(mapped);

        if (video.Name != mapped.Name &&
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

    public async Task<Result> DeleteVideoByIdAsync(Guid id)
    {
        var video = await _repository.FindFirstAsync(x => x.Id == id);

        if (video == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        await _repository.RemoveAsync(video);

        return Result.Ok();
    }
}