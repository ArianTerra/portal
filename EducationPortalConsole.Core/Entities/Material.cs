using EducationPortalConsole.Core.Entities.ManyToManyTables;
using EducationPortalConsole.Core.Entities.Progress;

namespace EducationPortalConsole.Core.Entities;

public class Material : AuditedEntity
{
    public ICollection<CourseMaterial> CourseMaterials { get; set; }

    public IEnumerable<MaterialProgress> MaterialProgresses { get; set; }
}