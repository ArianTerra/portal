using EducationPortalConsole.Core.Entities.ManyToManyTables;
using EducationPortalConsole.Core.Entities.Progress;

namespace EducationPortalConsole.Core.Entities;

public class Course : AuditedEntity
{
    public string? Description { get; set; }

    public ICollection<CourseMaterial> CourseMaterials { get; set; }

    public ICollection<CourseSkill> CourseSkills { get; set; }

    public ICollection<CourseProgress> CourseProgresses { get; set; }

    public IEnumerable<Material> Materials => CourseMaterials.Where(cm => cm.Course == this).
        Select(cm => cm.Material);
}