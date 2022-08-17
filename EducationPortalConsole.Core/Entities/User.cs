using System.ComponentModel.DataAnnotations.Schema;
using EducationPortalConsole.Core.Entities.Progress;

namespace EducationPortalConsole.Core.Entities;

public class User : BaseEntity //TODO should it be IAuditedEntity or not??
{
    public User()
    {
        CreatedCourses = new HashSet<Course>();
        CreatedMaterials = new HashSet<Material>();
        // UpdatedCourses = new HashSet<Course>();
        // UpdatedMaterials = new HashSet<Material>();
    }

    public string PasswordHash { get; set; }

    public string PasswordHashSalt { get; set; }

    //public IEnumerable<CourseProgress> CoursesProgress { get; set; } //TODO this is for future features

    //public IEnumerable<MaterialProgress> MaterialsProgress { get; set; }

    //public IEnumerable<SkillProgress> SkillsProgress { get; set; }

    public ICollection<Course> CreatedCourses { get; set; }

    public ICollection<Material> CreatedMaterials { get; set; }
}