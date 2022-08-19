using EducationPortalConsole.Core.Entities.JoinEntities;
using EducationPortalConsole.Core.Entities.Progress;

namespace EducationPortalConsole.Core.Entities;

public class Material : AuditedEntity
{
    public string Name { get; set; }

    public ICollection<CourseMaterial> CourseMaterials { get; set; }

    public ICollection<MaterialProgress> MaterialProgresses { get; set; }
}