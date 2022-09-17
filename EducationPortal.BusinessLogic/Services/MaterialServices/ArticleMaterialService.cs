using System.Linq.Expressions;
using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Resources.ErrorMessages;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.BusinessLogic.Validators.FluentValidation;
using EducationPortal.DataAccess.DomainModels.Materials;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Task1.DataAccess.Repository;

namespace EducationPortal.BusinessLogic.Services.MaterialServices;

public class ArticleMaterialService : IArticleMaterialService
{
    private readonly IGenericRepository<ArticleMaterial> _repository;

    //private readonly IValidator<ArticleMaterial> _validator;

    private readonly IMapper _mapper;

    public ArticleMaterialService(IGenericRepository<ArticleMaterial> repository, IMapper mapper)
    {
        _repository = repository;
        //_validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<ArticleMaterialDto>> GetArticleByIdAsync(Guid id)
    {
        var article = await _repository.FindFirstAsync(
                x => x.Id == id
        );

        if (article == null)
        {
            return Result.Fail(new NotFoundError(id));
        }

        var articleDto = _mapper.Map<ArticleMaterialDto>(article);

        return Result.Ok(articleDto);
    }

    public async Task<Result<IEnumerable<ArticleMaterialDto>>> GetArticlesPage(int page, int pageSize)
    {
        int itemCount = await _repository.CountAsync();

        if (pageSize <= 0)
        {
            return Result.Fail(new BadRequestError("Page size should be bigger than 0"));
        }

        if (page <= 0)
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

    // public Result<List<ArticleMaterial>> GetAllArticles()
    // {
    //     var result = Result.Try(() =>
    //         _repository.FindAll(
    //         _ => true,
    //         true,
    //         x => x.CreatedBy,
    //         x => x.UpdatedBy).ToList());
    //
    //     return result;
    // }
    //
    // public Result AddArticle(ArticleMaterial article)
    // {
    //     var validationResult = ValidateArticle(article);
    //
    //     return validationResult.IsSuccess
    //         ? Result.Try(() => _repository.Add(article))
    //         : validationResult;
    // }
    //
    // public Result UpdateArticle(ArticleMaterial article)
    // {
    //     var validationResult = ValidateArticle(article);
    //
    //     return validationResult.IsSuccess
    //         ? Result.Try(() => _repository.Update(article))
    //         : validationResult;
    // }
    //
    // public Result DeleteArticle(ArticleMaterial article)
    // {
    //     if (article == null)
    //     {
    //         return Result.Fail(ErrorMessages.ModelIsNull);
    //     }
    //
    //     return Result.Try(() => _repository.Remove(article));
    // }
    //
    // private Result ValidateArticle(ArticleMaterial article)
    // {
    //     if (article == null)
    //     {
    //         return Result.Fail(ErrorMessages.ModelIsNull);
    //     }
    //
    //     var validator = new ArticleMaterialValidator();
    //
    //     try
    //     {
    //         validator.ValidateAndThrow(article);
    //     }
    //     catch (Exception e)
    //     {
    //         return Result.Fail(new Error(ErrorMessages.ValidationError).CausedBy(e));
    //     }
    //
    //     return Result.Ok();
    // }
}