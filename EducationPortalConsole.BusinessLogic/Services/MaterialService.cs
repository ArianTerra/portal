using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services;

public class MaterialService : IMaterialService
{
    private readonly IMaterialRepository _repository;

    public MaterialService()
    {
        _repository = new MaterialRepository();
    }

    public MaterialService(IMaterialRepository repository)
    {
        _repository = repository;
    }

    public Material? GetById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id);
    }

    public IEnumerable<Material> GetAll()
    {
        return _repository.GetAll();
    }

    public void Add([NotNull] Material material)
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