using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services.MaterialServices;

public class ArticleMaterialService
{
    private readonly IGenericRepository<ArticleMaterial> _repository;

    public ArticleMaterialService()
    {
        _repository = new GenericRepository<ArticleMaterial>();
    }

    public ArticleMaterial? GeArticleById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public IEnumerable<ArticleMaterial> GetAllArticles()
    {
        return _repository.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public void AddArticle(ArticleMaterial material)
    {
        _repository.Add(material);
    }

    public void UpdateArticle(ArticleMaterial material)
    {
        _repository.Update(material);
    }

    public bool DeleteArticle(ArticleMaterial material)
    {
        return _repository.Remove(material);
    }
}