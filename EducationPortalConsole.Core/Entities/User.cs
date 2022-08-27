using EducationPortalConsole.Core.Entities.Progress;

namespace EducationPortalConsole.Core.Entities;

public class User : BaseEntity //TODO should it be IAuditedEntity or not??
{
    public string Name { get; set; }

    public string PasswordHash { get; set; }

    public string PasswordHashSalt { get; set; }

    public ICollection<CourseProgress> CourseProgresses { get; set; }

    public ICollection<MaterialProgress> MaterialProgresses { get; set; }

    public ICollection<SkillProgress> SkillProgresses { get; set; }

    public ICollection<Course> CreatedCourses { get; set; }

    public ICollection<Material> CreatedMaterials { get; set; }
}