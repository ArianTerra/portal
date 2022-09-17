using EducationPortal.DataAccess.DomainModels.JoinEntities;
using EducationPortal.DataAccess.DomainModels.Progress;

namespace EducationPortal.DataAccess.DomainModels;

public class Material : AuditedEntity
{
    public string Name { get; set; }

    public ICollection<CourseMaterial> CourseMaterials { get; set; }

    public ICollection<MaterialProgress> MaterialProgresses { get; set; }

    public override string ToString()
    {
        return Name;
    }
}