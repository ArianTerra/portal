using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.DataAccess.Repositories;

public class MaterialRepository : GenericRepository<Material>, IMaterialRepository
{
    public MaterialRepository() : base()
    {
    }
}