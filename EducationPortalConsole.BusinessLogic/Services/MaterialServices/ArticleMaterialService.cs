using EducationPortalConsole.BusinessLogic.Resources.ErrorMessages;
using EducationPortalConsole.BusinessLogic.Validators;
using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.DataAccess.Repositories;
using FluentResults;
using FluentValidation;

namespace EducationPortalConsole.BusinessLogic.Services.MaterialServices;

public class ArticleMaterialService
{
    private readonly IGenericRepository<ArticleMaterial> _repository;

    public ArticleMaterialService()
    {
        _repository = new GenericRepository<ArticleMaterial>();
    }

    public ArticleMaterialService(GenericRepository<ArticleMaterial> repository)
    {
        _repository = repository;
    }

    public Result<ArticleMaterial> GeArticleById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(ErrorMessages.MaterialGuidEmpty);
        }

        var result = Result.Try(() =>
            _repository.FindFirst(x => x.Id == id,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy));

        if (result.IsSuccess && result.Value == null)
        {
            return Result.Fail(ErrorMessages.MaterialNotFound);
        }

        return result;
    }

    public Result<List<ArticleMaterial>> GetAllArticles()
    {
        var result = Result.Try(() =>
            _repository.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy).ToList());

        return result;
    }

    public Result AddArticle(ArticleMaterial article)
    {
        var validationResult = ValidateArticle(article);

        return validationResult.IsSuccess
            ? Result.Try(() => _repository.Add(article))
            : validationResult;
    }

    public Result UpdateArticle(ArticleMaterial article)
    {
        var validationResult = ValidateArticle(article);

        return validationResult.IsSuccess
            ? Result.Try(() => _repository.Update(article))
            : validationResult;
    }

    public Result DeleteArticle(ArticleMaterial article)
    {
        if (article == null)
        {
            return Result.Fail(ErrorMessages.MaterialIsNull);
        }

        return Result.Try(() => _repository.Remove(article));
    }

    private Result ValidateArticle(ArticleMaterial article)
    {
        if (article == null)
        {
            return Result.Fail(ErrorMessages.MaterialIsNull);
        }

        var validator = new ArticleMaterialValidator();

        try
        {
            validator.ValidateAndThrow(article);
        }
        catch (Exception e)
        {
            return Result.Fail(new Error(ErrorMessages.ArticleMaterialValidationError).CausedBy(e));
        }

        return Result.Ok();
    }
}