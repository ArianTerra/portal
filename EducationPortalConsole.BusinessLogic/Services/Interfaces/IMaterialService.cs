using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface IMaterialService
{
    Material? GetFirst(Func<Material, bool> predicate);

    IEnumerable<Material> FindAll(Func<Material, bool> predicate);

    IEnumerable<Material> GetAll();

    void Add(Material entity);

    void Update(Material entity);

    bool Delete(Material entity);
}