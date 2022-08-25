using EducationPortalConsole.BusinessLogic.Services.MaterialServices;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.Materials;
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
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public IEnumerable<Material> GetAll()
    {
        return _repository.FindAll(
            _ => true,
            true,
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
        if (material is BookMaterial book)
        {
            return (new BookMaterialService()).Delete(book);
        }

        return _repository.Remove(material);
    }
}