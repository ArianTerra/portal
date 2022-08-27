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

    public Material? GetMaterialById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public IEnumerable<Material> GetAllMaterials()
    {
        return _repository.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public void AddMaterial(Material material)
    {
        _repository.Add(material);
    }

    public void UpdateMaterial(Material material)
    {
        _repository.Update(material);
    }

    public bool DeleteMaterial(Material material)
    {
        if (material is BookMaterial book)
        {
            return (new BookMaterialService()).DeleteBook(book);
        }

        return _repository.Remove(material);
    }
}