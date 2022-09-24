using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface IArticleMaterialService
{
    Task<Result<ArticleMaterialDto>> GetArticleByIdAsync(Guid id);

    Task<Result<IEnumerable<ArticleMaterialDto>>> GetArticlesPageAsync(int page, int pageSize);

    Task<Result<int>> GetArticlesCountAsync();

    Task<Result<Guid>> AddArticleAsync(ArticleMaterialDto dto, Guid createdById);

    Task<Result> UpdateArticleAsync(ArticleMaterialDto dto, Guid updatedById);

    Task<Result> DeleteArticleByIdAsync(Guid id);
}