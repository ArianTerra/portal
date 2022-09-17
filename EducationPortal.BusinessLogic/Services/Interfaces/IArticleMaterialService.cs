using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface IArticleMaterialService
{
    Task<Result<ArticleMaterialDto>> GetArticleByIdAsync(Guid id);

    Task<Result<IEnumerable<ArticleMaterialDto>>> GetArticlesPage(int page, int pageSize);
}