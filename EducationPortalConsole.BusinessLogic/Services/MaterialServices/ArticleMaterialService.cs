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

    public ArticleMaterial? GetById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public IEnumerable<ArticleMaterial> GetAll()
    {
        return _repository.GetAll(
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public void Add(ArticleMaterial material)
    {
        _repository.Add(material);
    }

    public void Update(ArticleMaterial material)
    {
        _repository.Update(material);
    }

    public bool Delete(ArticleMaterial material)
    {
        return _repository.Delete(material);
    }
}