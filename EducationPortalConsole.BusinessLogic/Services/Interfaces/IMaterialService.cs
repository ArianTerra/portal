using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface IMaterialService
{
    Material? GetById(Guid id);

    IEnumerable<Material> GetAll();

    void Add(Material material);

    void Update(Material material);

    bool Delete(Material material);
}