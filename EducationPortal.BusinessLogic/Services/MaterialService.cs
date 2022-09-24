using System.Linq.Expressions;
using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.BusinessLogic.Services;

public class MaterialService : IMaterialService
{
    private readonly IGenericRepository<Material> _repository;

    private readonly IMapper _mapper;

    public MaterialService(IGenericRepository<Material> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<MaterialDto>> GetMaterialByIdAsync(Guid id)
    {
        var material = await _repository.FindFirstAsync(
            filter: x => x.Id == id,
            tracking: true,
            includes: new Expression<Func<Material, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy,
            }
        );

        if (material == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var materialDto = _mapper.Map<MaterialDto>(material);

        return Result.Ok(materialDto);
    }

    public async Task<Result<IEnumerable<MaterialDto>>> GetMaterialsPageAsync(int page, int pageSize, string? nameStartsWith = null)
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

        var materialPage = await _repository.FindAll(
            filter: string.IsNullOrWhiteSpace(nameStartsWith) ? _ => true : x => x.Name.StartsWith(nameStartsWith),
            page: page,
            pageSize: pageSize,
            includes: new Expression<Func<Material, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy,
            }
        ).ToListAsync();

        var mapped = _mapper.Map<List<Material>, IEnumerable<MaterialDto>>(materialPage);

        return Result.Ok(mapped);
    }

    public async Task<Result<int>> GetMaterialsCountAsync(string? nameStartsWith = null)
    {
        return string.IsNullOrWhiteSpace(nameStartsWith)
            ? await _repository.CountAsync()
            : await _repository.CountAsync(x => x.Name.StartsWith(nameStartsWith));
    }
}