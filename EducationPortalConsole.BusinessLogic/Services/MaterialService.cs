using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services;

public class MaterialService : IMaterialService
{
    private readonly IGenericRepository<Material> _repository;

    public MaterialService()
    {
        _repository = new GenericRepository<Material>();
    }

    public MaterialService(IGenericRepository<Material> repository)
    {
        _repository = repository;
    }

    public Material? GetById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public IEnumerable<Material> GetAll()
    {
        return _repository.GetAll(
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public void Add(Material material)
    {
        _repository.Add(material);
    }

    public void Update(Material material)
    {
        _repository.Update(material);
    }

    public bool Delete(Material material)
    {
        return _repository.Delete(material);
    }
}