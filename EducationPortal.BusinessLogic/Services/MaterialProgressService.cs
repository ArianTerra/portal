using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.DataAccess.DomainModels.Progress;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.BusinessLogic.Services;

public class MaterialProgressService : IMaterialProgressService
{
    private readonly IGenericRepository<MaterialProgress> _progressRepository;

    private readonly IMapper _mapper;

    public MaterialProgressService(IGenericRepository<MaterialProgress> progressRepository, IMapper mapper)
    {
        _progressRepository = progressRepository;
        _mapper = mapper;
    }

    public async Task<Result> SetProgressAsync(Guid materialId, Guid userId, int percent)
    {
        if (percent < 0 || percent > 100)
        {
            return Result.Fail(new BadRequestError("Percent is incorrect"));
        }

        var progress = await _progressRepository.FindFirstAsync(
            x => x.MaterialId == materialId && x.UserId == userId
        );

        if (progress == null)
        {
            progress = new MaterialProgress()
            {
                UserId = userId,
                MaterialId = materialId,
            };

            await _progressRepository.AddAsync(progress);
        }

        progress.Progress = percent;
        await _progressRepository.UpdateAsync(progress);

        return Result.Ok();
    }

    public async Task<Result<IEnumerable<MaterialProgressDto>>> GetMaterialsProgressAsync(Guid userId)
    {
        var result = await _progressRepository.FindAll(x => x.UserId == userId).ToListAsync();

        var mapped = _mapper.Map<List<MaterialProgress>, IEnumerable<MaterialProgressDto>>(result);

        return Result.Ok(mapped);
    }
}