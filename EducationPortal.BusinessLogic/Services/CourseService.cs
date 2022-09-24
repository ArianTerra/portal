using System.Linq.Expressions;
using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.BusinessLogic.Utils.Comparers;
using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.DomainModels.JoinEntities;
using EducationPortal.DataAccess.DomainModels.Materials;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.BusinessLogic.Services;

public class CourseService : ICourseService
{
    private readonly IGenericRepository<Course> _repositoryCourses;

    private readonly IGenericRepository<Material> _repositoryMaterials;

    private readonly IGenericRepository<Skill> _repositorySkills;

    private readonly IGenericRepository<CourseMaterial> _repositoryCourseMaterials;

    private readonly IGenericRepository<CourseSkill> _repositoryCourseSkills;

    private readonly IMapper _mapper;

    private readonly IValidator<Course> _validator;

    public CourseService(IGenericRepository<Course> repositoryCourses,
        IGenericRepository<Material> repositoryMaterials,
        IGenericRepository<Skill> repositorySkills,
        IGenericRepository<CourseMaterial> repositoryCourseMaterials,
        IGenericRepository<CourseSkill> repositoryCourseSkills,
        IMapper mapper,
        IValidator<Course> validator)
    {
        _repositoryCourses = repositoryCourses;
        _repositoryMaterials = repositoryMaterials;
        _repositorySkills = repositorySkills;
        _repositoryCourseMaterials = repositoryCourseMaterials;
        _repositoryCourseSkills = repositoryCourseSkills;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<CourseDto>> GetCourseByIdAsync(Guid id)
    {
        var course = await _repositoryCourses.FindFirstAsync(
            filter: x => x.Id == id,
            includes: new Expression<Func<Course, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy
            }
        );

        if (course == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var courseDto = _mapper.Map<CourseDto>(course);
        courseDto.Materials = await GetMaterialsAsync(id);
        courseDto.Skills = await GetSkillsAsync(id);

        return Result.Ok(courseDto);
    }

    public async Task<Result<IEnumerable<CourseDto>>> GetCoursePageAsync(int page, int pageSize)
    {
        int itemsCount = await _repositoryCourses.CountAsync();
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

        var videoPage = await _repositoryCourses.FindAll(
            page: page,
            pageSize: pageSize,
            includes: new Expression<Func<Course, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy,
            }
        ).ToListAsync();

        var mapped = _mapper.Map<List<Course>, IEnumerable<CourseDto>>(videoPage);

        return Result.Ok(mapped);
    }

    public async Task<Result<int>> GetCoursesCountAsync()
    {
        return await _repositoryCourses.CountAsync();
    }

    public async Task<Result<Guid>> AddCourseAsync(CourseDto dto, Guid createdById)
    {
        if (dto == null)
        {
            return Result.Fail(new BadRequestError($"{nameof(CourseDto)} is null"));
        }

        var mapped = _mapper.Map<Course>(dto);
        mapped.CreatedById = createdById;
        mapped.CreatedOn = DateTime.Now;

        if (mapped.Id != Guid.Empty &&
            await _repositoryCourses.FindFirstAsync(x => x.Id == mapped.Id) != null)
        {
            return Result.Fail(new BadRequestError($"Course with Id {mapped.Id} already exist in database"));
        }

        var validationResult = await _validator.ValidateAsync(mapped);

        if (await _repositoryCourses.FindFirstAsync(x => x.Name == mapped.Name) != null)
        {
            validationResult.Errors.Add(
                new ValidationFailure(nameof(mapped.Name), "Name must be unique"));
        }

        if (!validationResult.IsValid)
        {
            return Result.Fail(new ValidationError(validationResult));
        }

        await _repositoryCourses.AddAsync(mapped);

        return Result.Ok(mapped.Id);
    }

    public async Task<Result> UpdateCourseAsync(CourseDto dto, Guid updatedById)
    {
        var course = await _repositoryCourses.FindFirstAsync(x => x.Id == dto.Id);

        if (course == null)
        {
            return Result.Fail(new NotFoundError(dto.Id));
        }

        var mapped = _mapper.Map<Course>(dto);
        mapped.CreatedById = course.CreatedById;
        mapped.CreatedOn = course.CreatedOn;
        mapped.UpdatedById = updatedById;
        mapped.UpdatedOn = DateTime.Now;

        var validationResult = await _validator.ValidateAsync(mapped);

        if (course.Name != mapped.Name &&
            await _repositoryCourses.FindFirstAsync(x => x.Name == mapped.Name) != null)
        {
            validationResult.Errors.Add(
                new ValidationFailure(nameof(mapped.Name), "Name must be unique"));
        }

        if (!validationResult.IsValid)
        {
            return Result.Fail(new ValidationError(validationResult));
        }

        await _repositoryCourses.UpdateAsync(mapped);

        return Result.Ok();
    }

    public async Task<Result> DeleteCourseByIdAsync(Guid id)
    {
        var book = await _repositoryCourses.FindFirstAsync(x => x.Id == id);

        if (book == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        await _repositoryCourses.RemoveAsync(book);

        return Result.Ok();
    }

    public async Task<Result> AddMaterialsToCourseAsync(Guid courseId, IEnumerable<MaterialDto> materials)
    {
        var course = await _repositoryCourses.FindFirstAsync(
            filter: x => x.Id == courseId,
            includes: new Expression<Func<Course, object>>[]
            {
                x => x.CourseMaterials
            });

        if (course == null)
        {
            return Result.Fail(new NotFoundError(courseId));
        }

        var oldLinks = course.CourseMaterials.Where(x => x.CourseId == course.Id);
        var newLinks = materials.Select(material => new CourseMaterial() { CourseId = course.Id, MaterialId = material.Id }).ToList();

        var comparer = new CourseMaterialComparer();
        var linksToDelete = oldLinks.Except(newLinks, comparer).ToList();
        var linksToAdd = newLinks.Except(oldLinks, comparer).ToList();

        await _repositoryCourseMaterials.RemoveRangeAsync(linksToDelete);
        await _repositoryCourseMaterials.AddRangeAsync(linksToAdd);

        return Result.Ok();
    }

    public async Task<Result> AddSkillsToCourseAsync(Guid id, IEnumerable<SkillDto> skills)
    {
        var course = await _repositoryCourses.FindFirstAsync(
            filter: x => x.Id == id,
            includes: new Expression<Func<Course, object>>[]
            {
                x => x.CourseSkills
            });

        if (course == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var oldLinks = course.CourseSkills.Where(x => x.CourseId == course.Id);
        var newLinks = skills.Select(skill => new CourseSkill() { CourseId = course.Id, SkillId = skill.Id }).ToList();

        var comparer = new CourseSkillComparer();
        var linksToDelete = oldLinks.Except(newLinks, comparer).ToList();
        var linksToAdd = newLinks.Except(oldLinks, comparer).ToList();

        await _repositoryCourseSkills.RemoveRangeAsync(linksToDelete);
        await _repositoryCourseSkills.AddRangeAsync(linksToAdd);

        return Result.Ok();
    }

    private async Task<IEnumerable<MaterialDto>> GetMaterialsAsync(Guid id)
    {
        var course = await _repositoryCourses.FindFirstAsync(
            filter: x => x.Id == id,
            includes: new Expression<Func<Course, object>>[]
            {
                x => x.CourseMaterials
            }
        );

        var materialIds = course.CourseMaterials.Select(x => x.MaterialId);

        var materials = new List<MaterialDto>();

        foreach (var materialId in materialIds)
        {
            var material = await _repositoryMaterials.FindFirstAsync(x => x.Id == materialId);
            materials.Add(_mapper.Map<MaterialDto>(material));
        }

        return materials;
    }

    private async Task<IEnumerable<SkillDto>> GetSkillsAsync(Guid id)
    {
        var course = await _repositoryCourses.FindFirstAsync(
            filter: x => x.Id == id,
            includes: new Expression<Func<Course, object>>[]
            {
                x => x.CourseSkills
            }
        );

        var skillIds = course.CourseSkills.Select(x => x.SkillId);

        var skills = new List<SkillDto>();

        foreach (var skillId in skillIds)
        {
            var skill = await _repositorySkills.FindFirstAsync(x => x.Id == skillId);
            skills.Add(_mapper.Map<SkillDto>(skill));
        }

        return skills;
    }
}