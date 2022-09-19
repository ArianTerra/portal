using System.Linq.Expressions;
using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.DataAccess.DomainModels.Materials;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Task1.DataAccess.Repository;

namespace EducationPortal.BusinessLogic.Services.MaterialServices;

public class ArticleMaterialService : IArticleMaterialService
{
    private readonly IGenericRepository<ArticleMaterial> _repository;

    private readonly IMapper _mapper;

    private readonly IValidator<ArticleMaterial> _validator;

    public ArticleMaterialService(IGenericRepository<ArticleMaterial> repository, IMapper mapper, IValidator<ArticleMaterial> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<ArticleMaterialDto>> GetArticleByIdAsync(Guid id)
    {
        var article = await _repository.FindFirstAsync(
                filter: x => x.Id == id,
                tracking: true,
                includes: new Expression<Func<ArticleMaterial, object>>[]
                {
                    x => x.CreatedBy,
                    x => x.UpdatedBy
                }
        );

        if (article == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var articleDto = _mapper.Map<ArticleMaterialDto>(article);

        return Result.Ok(articleDto);
    }

    public async Task<Result<IEnumerable<ArticleMaterialDto>>> GetArticlesPageAsync(int page, int pageSize)
    {
        int itemsCount = await _repository.CountAsync();
        int pagesCount = (int)Math.Ceiling((double)itemsCount / pageSize);

        if (pageSize <= 0)
        {
            return Result.Fail(new BadRequestError("Page size should be bigger than 0"));
        }

        if (page <= 0 || page > pagesCount)
        {
            return Result.Fail(new BadRequestError("Page does not exist"));
        }

        var articlePage = await _repository.FindAll(
            page: page,
            pageSize: pageSize,
            includes: new Expression<Func<ArticleMaterial, object>>[]
            {
                x => x.CreatedBy,
                x => x.UpdatedBy
            }
        ).ToListAsync();

        var mapped = _mapper.Map<List<ArticleMaterial>, IEnumerable<ArticleMaterialDto>>(articlePage);

        return Result.Ok(mapped);
    }

    public async Task<Result<int>> GetArticlesCountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<Result<Guid>> AddArticleAsync(ArticleMaterialDto dto)
    {
        if (dto == null)
        {
            return Result.Fail(new BadRequestError("ArticleMaterialDto is null"));
        }

        var mapped = _mapper.Map<ArticleMaterial>(dto);

        if (mapped.Id != Guid.Empty &&
            await _repository.FindFirstAsync(x => x.Id == mapped.Id) != null)
        {
            return Result.Fail(new BadRequestError($"Article with Id {mapped.Id} already exist in database"));
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

    public async Task<Result> UpdateArticleAsync(ArticleMaterialDto dto)
    {
        var article = await _repository.FindFirstAsync(x => x.Id == dto.Id);

        if (article == null)
        {
            return Result.Fail(new NotFoundError(dto.Id));
        }

        var mapped = _mapper.Map<ArticleMaterial>(dto);

        var validationResult = await _validator.ValidateAsync(mapped);

        if (article.Name != mapped.Name &&
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

    public async Task<Result> DeleteArticleByIdAsync(Guid id)
    {
        var article = await _repository.FindFirstAsync(x => x.Id == id);

        if (article == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        await _repository.RemoveAsync(article);

        return Result.Ok();
    }
}