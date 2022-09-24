using System.Linq.Expressions;
using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.BusinessLogic.Services;

public class SkillService : ISkillService
{
    private readonly IGenericRepository<Skill> _repository;

    private readonly IMapper _mapper;

    private readonly IValidator<Skill> _validator;

    public SkillService(IGenericRepository<Skill> repository, IMapper mapper, IValidator<Skill> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<SkillDto>> GetSkillByIdAsync(Guid id)
    {
        var skill = await _repository.FindFirstAsync(
            filter: x => x.Id == id,
            tracking: true,
            includes: new Expression<Func<Skill, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy,
            }
        );

        if (skill == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var skillDto = _mapper.Map<SkillDto>(skill);

        return Result.Ok(skillDto);
    }

    public async Task<Result<IEnumerable<SkillDto>>> GetSkillsPageAsync(int page, int pageSize)
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

        var skillPage = await _repository.FindAll(
            page: page,
            pageSize: pageSize,
            includes: new Expression<Func<Skill, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy,
            }
        ).ToListAsync();

        var mapped = _mapper.Map<List<Skill>, IEnumerable<SkillDto>>(skillPage);

        return Result.Ok(mapped);
    }

    public async Task<Result<int>> GetSkillsCountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<Result<Guid>> AddSkillAsync(SkillDto dto, Guid createdById)
    {
        if (dto == null)
        {
            return Result.Fail(new BadRequestError($"{nameof(SkillDto)} is null"));
        }

        var mapped = _mapper.Map<Skill>(dto);
        mapped.CreatedById = createdById;
        mapped.CreatedOn = DateTime.Now;

        if (mapped.Id != Guid.Empty &&
            await _repository.FindFirstAsync(x => x.Id == mapped.Id) != null)
        {
            return Result.Fail(new BadRequestError($"Skill with Id {mapped.Id} already exist in database"));
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

    public async Task<Result> UpdateSkillAsync(SkillDto dto, Guid updatedById)
    {
        var skill = await _repository.FindFirstAsync(x => x.Id == dto.Id);

        if (skill == null)
        {
            return Result.Fail(new NotFoundError(dto.Id));
        }

        var mapped = _mapper.Map<Skill>(dto);
        mapped.CreatedById = skill.CreatedById;
        mapped.CreatedOn = skill.CreatedOn;
        mapped.UpdatedById = updatedById;
        mapped.UpdatedOn = DateTime.Now;

        var validationResult = await _validator.ValidateAsync(mapped);

        if (skill.Name != mapped.Name &&
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

    public async Task<Result> DeleteSkillByIdAsync(Guid id)
    {
        var skill = await _repository.FindFirstAsync(x => x.Id == id);

        if (skill == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        await _repository.RemoveAsync(skill);

        return Result.Ok();
    }
}