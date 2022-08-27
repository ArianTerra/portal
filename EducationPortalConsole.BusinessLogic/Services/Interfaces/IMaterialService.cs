using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface IMaterialService
{
    Material? GetMaterialById(Guid id);

    IEnumerable<Material> GetAllMaterials();

    void AddMaterial(Material material);

    void UpdateMaterial(Material material);

    bool DeleteMaterial(Material material);
}