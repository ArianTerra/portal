using System.ComponentModel.DataAnnotations.Schema;
using EducationPortalConsole.Core.Entities.Progress;

namespace EducationPortalConsole.Core.Entities;

public class User : BaseEntity //TODO should it be IAuditedEntity or not??
{
    public string PasswordHash { get; set; }

    public string PasswordHashSalt { get; set; }

    public IEnumerable<CourseProgress> CourseProgresses { get; set; }

    public IEnumerable<MaterialProgress> MaterialProgresses { get; set; }

    public IEnumerable<SkillProgress> SkillProgresses { get; set; }

    public ICollection<Course> CreatedCourses { get; set; }

    public ICollection<Material> CreatedMaterials { get; set; }
}