using EducationPortal.DataAccess.DomainModels.Progress;
using Microsoft.AspNetCore.Identity;

namespace EducationPortal.DataAccess.DomainModels;

public class ApplicationUser : IdentityUser<Guid>
{
    public ICollection<CourseProgress> CourseProgresses { get; set; }

    public ICollection<MaterialProgress> MaterialProgresses { get; set; }

    public ICollection<SkillProgress> SkillProgresses { get; set; }

    public ICollection<Course> CreatedCourses { get; set; }

    public ICollection<Material> CreatedMaterials { get; set; }
}