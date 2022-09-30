using EducationPortal.DataAccess.DomainModels.JoinEntities;
using EducationPortal.DataAccess.DomainModels.Progress;

namespace EducationPortal.DataAccess.DomainModels;

public class Course : AuditedEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<CourseMaterial> CourseMaterials { get; set; }

    public ICollection<CourseSkill> CourseSkills { get; set; }

    public ICollection<CourseProgress> CourseProgresses { get; set; }

    // public IEnumerable<Material> Materials => CourseMaterials.Where(cm => cm.Course == this).
    //     Select(cm => cm.Material);
}